using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;


[RequireComponent(typeof(Animator))]
public class BodyCtrl : MonoBehaviour
{
    // Bool that has the characters (facing the player) actions become mirrored. Default false.
    public bool mirroredMovement = false;

    // Bool that determines whether the avatar is allowed to move in vertical direction.
    public bool verticalMovement = false;

    // Rate at which avatar will move through the scene. The rate multiplies the movement speed (.001f, i.e dividing by 1000, unity's framerate).
    protected int moveRate = 1;

    // Slerp smooth factor
    public float smoothFactor = 5f;

    // Whether the offset node must be repositioned to the user's coordinates, as reported by the sensor or not.F
    public bool offsetRelativeToSensor = false;


    //Mine
    public String fsPath;
    public FileStream fs;
    // StreamWriter for the stored data
    public StreamWriter sw;
    public int count = 0;
    //EndMine

    // The body root node
    protected Transform bodyRoot;

    // A required variable if you want to rotate the model in space.
    protected GameObject offsetNode;

    // Variable to hold all them bones. It will initialize the same size as initialRotations.
    protected Transform[] bones;

    // Rotations of the bones when the Kinect tracking starts.
    protected Quaternion[] initialRotations;
    protected Quaternion[] initialLocalRotations;

    // Initial position and rotation of the transform
    protected Vector3 initialPosition;
    protected Quaternion initialRotation;

    // Calibration Offset Variables for Character Position.
    protected bool offsetCalibrated = false;
    protected float xOffset, yOffset, zOffset;

    // transform caching gives performance boost since Unity calls GetComponent<Transform>() each time you call transform 
    private Transform _transformCache;
    public new Transform transform
    {
        get
        {
            if (!_transformCache)
                _transformCache = base.transform;

            return _transformCache;
        }
    }
    private GameObject body;
    private BodyCtrlData BCD;

    StreamReader sr;
    String Path = "E:\\Boneknitting2018-May-03-03-23-41.txt";

    void Awake()
    {
        Debug.Log("TestBody Awake!");
        initBodyCtrlData();
        sr = new StreamReader(Path, Encoding.Default);
    }

    public void initBodyCtrlData()
    {
        // check for double start
        if (bones != null)
            return;

        // inits the bones array
        bones = new Transform[22];

        // Initial rotations and directions of the bones.
        initialRotations = new Quaternion[bones.Length];
        initialLocalRotations = new Quaternion[bones.Length];

        // Map bones to the points the Kinect tracks
        MapBones();

        // Get initial bone rotations
        GetInitialRotations();
    }
    String DataString;
    // Update the avatar each frame.
    public void Update()
    {
        String DataString = sr.ReadLine();
        if (DataString == null) return;
        String[] parts = DataString.Split('\t');

        BCD.userPosition = new Vector3(float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3]));
        BCD.UserID = 2;
        BCD.jointRotation = new Quaternion[22];

        for (int i = 4, j = 0; j < 22; i += 4, j++)
        {
            BCD.jointRotation[j].x = float.Parse(parts[i]);
            BCD.jointRotation[j].y = float.Parse(parts[i + 1]);
            BCD.jointRotation[j].z = float.Parse(parts[i + 2]);
            BCD.jointRotation[j].w = float.Parse(parts[i + 3]);
        }
        /*
        foreach (String part in parts)
        {
            Debug.Log(part);
            //Debug.Log(Double.Parse(part)); ;
        }
        */
        MoveBody(BCD);
    }

    // Update the avatar each frame.
    public void MoveBody(BodyCtrlData bodyCtrlData)
    {
        if (!transform.gameObject.activeInHierarchy)
            return;

        // move the avatar to its Kinect position
        MoveAvatar(bodyCtrlData.UserID);

        for (var boneIndex = 0; boneIndex < bones.Length; boneIndex++)
        {
            if (!bones[boneIndex])
                continue;

            Transform boneTransform = bones[boneIndex];

            if (smoothFactor != 0f)
                boneTransform.rotation = Quaternion.Slerp(boneTransform.rotation, bodyCtrlData.jointRotation[boneIndex], smoothFactor * Time.deltaTime);
            else
                boneTransform.rotation = bodyCtrlData.jointRotation[boneIndex];
        }
    }

    // Set bones to their initial positions and rotations
    public void ResetToInitialPosition()
    {
        if (bones == null)
            return;

        if (offsetNode != null)
        {
            offsetNode.transform.rotation = Quaternion.identity;
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }

        // For each bone that was defined, reset to initial position.
        for (int i = 0; i < bones.Length; i++)
        {
            if (bones[i] != null)
            {
                bones[i].rotation = initialRotations[i];
            }
        }

        if (bodyRoot != null)
        {
            bodyRoot.localPosition = Vector3.zero;
            bodyRoot.localRotation = Quaternion.identity;
        }

        // Restore the offset's position and rotation
        if (offsetNode != null)
        {
            offsetNode.transform.position = initialPosition;
            offsetNode.transform.rotation = initialRotation;
        }
        else
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
        }
    }

    // Invoked on the successful calibration of a player.
    public void SuccessfulCalibration(uint userId)
    {
        // reset the models position
        if (offsetNode != null)
        {
            offsetNode.transform.rotation = initialRotation;
        }

        // re-calibrate the position offset
        offsetCalibrated = false;
    }


    // Moves the avatar in 3D space - pulls the tracked position of the spine and applies it to root.
    // Only pulls positional, not rotational.
    protected void MoveAvatar(uint UserID)
    {
        if (bodyRoot == null)
            return;

        // Get the position of the body and store it.
        Vector3 trans = BCD.userPosition;

        // If this is the first time we're moving the avatar, set the offset. Otherwise ignore it.
        if (!offsetCalibrated)
        {
            offsetCalibrated = true;

            xOffset = !mirroredMovement ? trans.x * moveRate : -trans.x * moveRate;
            yOffset = trans.y * moveRate;
            zOffset = -trans.z * moveRate;

            if (offsetRelativeToSensor)
            {
                Vector3 cameraPos = Camera.main.transform.position;

                float yRelToAvatar = (offsetNode != null ? offsetNode.transform.position.y : transform.position.y) - cameraPos.y;
                Vector3 relativePos = new Vector3(trans.x * moveRate, yRelToAvatar, trans.z * moveRate);
                Vector3 offsetPos = cameraPos + relativePos;

                if (offsetNode != null)
                {
                    offsetNode.transform.position = offsetPos;
                }
                else
                {
                    transform.position = offsetPos;
                }
            }
        }

        // Smoothly transition to the new position
        Vector3 targetPos = Kinect2AvatarPos(trans, verticalMovement);

        if (smoothFactor != 0f)
            bodyRoot.localPosition = Vector3.Lerp(bodyRoot.localPosition, targetPos, smoothFactor * Time.deltaTime);
        else
            bodyRoot.localPosition = targetPos;
    }

    // If the bones to be mapped have been declared, map that bone to the model.
    protected virtual void MapBones()
    {
        // make OffsetNode as a parent of model transform.
        offsetNode = new GameObject(name + "Ctrl") { layer = transform.gameObject.layer, tag = transform.gameObject.tag };
        offsetNode.transform.position = transform.position;
        offsetNode.transform.rotation = transform.rotation;
        offsetNode.transform.parent = transform.parent;

        transform.parent = offsetNode.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // take model transform as body root
        bodyRoot = transform;

        // get bone transforms from the animator component
        var animatorComponent = GetComponent<Animator>();

        for (int boneIndex = 0; boneIndex < bones.Length; boneIndex++)
        {
            if (!boneIndex2MecanimMap.ContainsKey(boneIndex))
                continue;

            bones[boneIndex] = animatorComponent.GetBoneTransform(boneIndex2MecanimMap[boneIndex]);
        }
    }

    // Capture the initial rotations of the bones
    protected void GetInitialRotations()
    {
        // save the initial rotation
        if (offsetNode != null)
        {
            initialPosition = offsetNode.transform.position;
            initialRotation = offsetNode.transform.rotation;

            offsetNode.transform.rotation = Quaternion.identity;
        }
        else
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;

            transform.rotation = Quaternion.identity;
        }

        for (int i = 0; i < bones.Length; i++)
        {
            if (bones[i] != null)
            {
                initialRotations[i] = bones[i].rotation; // * Quaternion.Inverse(initialRotation);
                initialLocalRotations[i] = bones[i].localRotation;
            }
        }

        // Restore the initial rotation
        if (offsetNode != null)
        {
            offsetNode.transform.rotation = initialRotation;
        }
        else
        {
            transform.rotation = initialRotation;
        }
    }

    // Converts kinect joint rotation to avatar joint rotation, depending on joint initial rotation and offset rotation
    protected Quaternion Kinect2AvatarRot(Quaternion jointRotation, int boneIndex)
    {
        // Apply the new rotation.
        Quaternion newRotation = jointRotation * initialRotations[boneIndex];

        //If an offset node is specified, combine the transform with its
        //orientation to essentially make the skeleton relative to the node
        if (offsetNode != null)
        {
            // Grab the total rotation by adding the Euler and offset's Euler.
            Vector3 totalRotation = newRotation.eulerAngles + offsetNode.transform.rotation.eulerAngles;
            // Grab our new rotation.
            newRotation = Quaternion.Euler(totalRotation);
        }

        return newRotation;
    }

    // Converts Kinect position to avatar skeleton position, depending on initial position, mirroring and move rate
    protected Vector3 Kinect2AvatarPos(Vector3 jointPosition, bool bMoveVertically)
    {
        float xPos;
        float yPos;
        float zPos;

        // If movement is mirrored, reverse it.
        if (!mirroredMovement)
            xPos = jointPosition.x * moveRate - xOffset;
        else
            xPos = -jointPosition.x * moveRate - xOffset;

        yPos = jointPosition.y * moveRate - yOffset;
        zPos = -jointPosition.z * moveRate - zOffset;

        // If we are tracking vertical movement, update the y. Otherwise leave it alone.
        Vector3 avatarJointPos = new Vector3(xPos, bMoveVertically ? yPos : 0f, zPos);

        return avatarJointPos;
    }

    // dictionaries to speed up bones' processing
    // the author of the terrific idea for kinect-joints to mecanim-bones mapping
    // along with its initial implementation, including following dictionary is
    // Mikhail Korchun (korchoon@gmail.com). Big thanks to this guy!
    private readonly Dictionary<int, HumanBodyBones> boneIndex2MecanimMap = new Dictionary<int, HumanBodyBones>
    {
        {0, HumanBodyBones.Hips},
        {1, HumanBodyBones.Spine},
        {2, HumanBodyBones.Neck},
        {3, HumanBodyBones.Head},

        {4, HumanBodyBones.LeftShoulder},
        {5, HumanBodyBones.LeftUpperArm},
        {6, HumanBodyBones.LeftLowerArm},
        {7, HumanBodyBones.LeftHand},
        {8, HumanBodyBones.LeftIndexProximal},

        {9, HumanBodyBones.RightShoulder},
        {10, HumanBodyBones.RightUpperArm},
        {11, HumanBodyBones.RightLowerArm},
        {12, HumanBodyBones.RightHand},
        {13, HumanBodyBones.RightIndexProximal},

        {14, HumanBodyBones.LeftUpperLeg},
        {15, HumanBodyBones.LeftLowerLeg},
        {16, HumanBodyBones.LeftFoot},
        {17, HumanBodyBones.LeftToes},

        {18, HumanBodyBones.RightUpperLeg},
        {19, HumanBodyBones.RightLowerLeg},
        {20, HumanBodyBones.RightFoot},
        {21, HumanBodyBones.RightToes},
    };

    protected readonly Dictionary<int, KinectWrapper.NuiSkeletonPositionIndex> boneIndex2JointMap = new Dictionary<int, KinectWrapper.NuiSkeletonPositionIndex>
    {
        {0, KinectWrapper.NuiSkeletonPositionIndex.HipCenter},
        {1, KinectWrapper.NuiSkeletonPositionIndex.Spine},
        {2, KinectWrapper.NuiSkeletonPositionIndex.ShoulderCenter},
        {3, KinectWrapper.NuiSkeletonPositionIndex.Head},

        {5, KinectWrapper.NuiSkeletonPositionIndex.ShoulderLeft},
        {6, KinectWrapper.NuiSkeletonPositionIndex.ElbowLeft},
        {7, KinectWrapper.NuiSkeletonPositionIndex.WristLeft},
        {8, KinectWrapper.NuiSkeletonPositionIndex.HandLeft},

        {10, KinectWrapper.NuiSkeletonPositionIndex.ShoulderRight},
        {11, KinectWrapper.NuiSkeletonPositionIndex.ElbowRight},
        {12, KinectWrapper.NuiSkeletonPositionIndex.WristRight},
        {13, KinectWrapper.NuiSkeletonPositionIndex.HandRight},

        {14, KinectWrapper.NuiSkeletonPositionIndex.HipLeft},
        {15, KinectWrapper.NuiSkeletonPositionIndex.KneeLeft},
        {16, KinectWrapper.NuiSkeletonPositionIndex.AnkleLeft},
        {17, KinectWrapper.NuiSkeletonPositionIndex.FootLeft},

        {18, KinectWrapper.NuiSkeletonPositionIndex.HipRight},
        {19, KinectWrapper.NuiSkeletonPositionIndex.KneeRight},
        {20, KinectWrapper.NuiSkeletonPositionIndex.AnkleRight},
        {21, KinectWrapper.NuiSkeletonPositionIndex.FootRight},
    };

    protected readonly Dictionary<int, List<KinectWrapper.NuiSkeletonPositionIndex>> specIndex2JointMap = new Dictionary<int, List<KinectWrapper.NuiSkeletonPositionIndex>>
    {
        {4, new List<KinectWrapper.NuiSkeletonPositionIndex> {KinectWrapper.NuiSkeletonPositionIndex.ShoulderLeft, KinectWrapper.NuiSkeletonPositionIndex.ShoulderCenter} },
        {9, new List<KinectWrapper.NuiSkeletonPositionIndex> {KinectWrapper.NuiSkeletonPositionIndex.ShoulderRight, KinectWrapper.NuiSkeletonPositionIndex.ShoulderCenter} },
    };

    protected readonly Dictionary<int, KinectWrapper.NuiSkeletonPositionIndex> boneIndex2MirrorJointMap = new Dictionary<int, KinectWrapper.NuiSkeletonPositionIndex>
    {
        {0, KinectWrapper.NuiSkeletonPositionIndex.HipCenter},
        {1, KinectWrapper.NuiSkeletonPositionIndex.Spine},
        {2, KinectWrapper.NuiSkeletonPositionIndex.ShoulderCenter},
        {3, KinectWrapper.NuiSkeletonPositionIndex.Head},

        {5, KinectWrapper.NuiSkeletonPositionIndex.ShoulderRight},
        {6, KinectWrapper.NuiSkeletonPositionIndex.ElbowRight},
        {7, KinectWrapper.NuiSkeletonPositionIndex.WristRight},
        {8, KinectWrapper.NuiSkeletonPositionIndex.HandRight},

        {10, KinectWrapper.NuiSkeletonPositionIndex.ShoulderLeft},
        {11, KinectWrapper.NuiSkeletonPositionIndex.ElbowLeft},
        {12, KinectWrapper.NuiSkeletonPositionIndex.WristLeft},
        {13, KinectWrapper.NuiSkeletonPositionIndex.HandLeft},

        {14, KinectWrapper.NuiSkeletonPositionIndex.HipRight},
        {15, KinectWrapper.NuiSkeletonPositionIndex.KneeRight},
        {16, KinectWrapper.NuiSkeletonPositionIndex.AnkleRight},
        {17, KinectWrapper.NuiSkeletonPositionIndex.FootRight},

        {18, KinectWrapper.NuiSkeletonPositionIndex.HipLeft},
        {19, KinectWrapper.NuiSkeletonPositionIndex.KneeLeft},
        {20, KinectWrapper.NuiSkeletonPositionIndex.AnkleLeft},
        {21, KinectWrapper.NuiSkeletonPositionIndex.FootLeft},
    };

    protected readonly Dictionary<int, List<KinectWrapper.NuiSkeletonPositionIndex>> specIndex2MirrorJointMap = new Dictionary<int, List<KinectWrapper.NuiSkeletonPositionIndex>>
    {
        {4, new List<KinectWrapper.NuiSkeletonPositionIndex> {KinectWrapper.NuiSkeletonPositionIndex.ShoulderRight, KinectWrapper.NuiSkeletonPositionIndex.ShoulderCenter} },
        {9, new List<KinectWrapper.NuiSkeletonPositionIndex> {KinectWrapper.NuiSkeletonPositionIndex.ShoulderLeft, KinectWrapper.NuiSkeletonPositionIndex.ShoulderCenter} },
    };

}

