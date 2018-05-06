using System.Collections.Generic;
using TrajectoryData;
using UnityEngine;

public class DrawCurvesWithLineRenderer : MonoBehaviour
{

    private int start_index = 0;
    private int end_index = 0;
    private int cur_traj = 0;
    private GameObject hand1;
    private GameObject hand2;
    private GameObject root1;
    private GameObject root2;
    private HandMotion motion = new HandMotion();
    private List<LineRenderer> lineRenderer = new List<LineRenderer>();
    private int lengthOfLineRenderer = 0;

    private float speed = 0.5F;
    private float distance = 1.0F;
    private bool isPlay = false;

    private bool button = false;


    // Use this for initialization
    void OnEnable()
    {
        motion = TrailCurveDrawCtrl.Instance().curMotion;
        //isPlay = TrailCurveDrawCtrl.Instance().getIsPlay();

        hand1 = new GameObject();
        hand2 = new GameObject();

        start_index = 0;
        end_index = (int)motion.getTraj(cur_traj).size();
        for (int i = 0; i < motion.size(); ++i)
        {
            initLineRenderer(i, Color.blue, Color.blue);
        }
        /*残影部分gameobject初始化
        root1 = GameObject.Find("GameObject1");
        root2 = GameObject.Find("GameObject2");
        hand1 = root1.transform.Find("lefthand").gameObject;
         **/
    }
    void Start()
    {

    }

    void initLineRenderer(int index, Color sc, Color ec, float sw = 0.5F, float ew = 0.5F)
    {
        string name = string.Format("Linerenderer{0}", index);
        lineRenderer[index] = GameObject.Find(name).GetComponent<LineRenderer>();
        lineRenderer[index].material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer[index].startColor = sc;
        lineRenderer[index].endColor = ec;
        lineRenderer[index].positionCount = (int)motion.getTraj(cur_traj).size();
        lineRenderer[index].startWidth = sw;
        lineRenderer[index].endWidth = ew;
    }
    // Update is called once per frame
    void Update()
    {
        drawCurve();
    }

    void drawCurve()
    {
        play();

        for (int traj = 0; traj < motion.size(); ++traj)
        {
            if (motion.getTraj(traj).getActive() == true)
            {
                lineRenderer[traj].positionCount = end_index;
                Vec3 temp = new Vec3();
                for (int pos = start_index; pos < end_index; ++pos)
                {
                    temp = motion.getTraj(traj).vec[pos].position;
                    lineRenderer[traj].SetPosition(pos, new Vector3(temp.x, temp.y, temp.z));
                }

            }
        }

        //drawGhost();
    }

    void drawGhost()
    {
        if (button)
        {
            Vector3 rotation = transform.localEulerAngles;
            hand1.transform.position = new Vector3(motion.getTraj(0).vec[end_index - 1].position.x, motion.getTraj(0).vec[end_index - 1].position.y, motion.getTraj(0).vec[end_index - 1].position.z);
            rotation = new Vector3(motion.getTraj(0).vec[end_index - 1].azimuth, motion.getTraj(0).vec[end_index - 1].elevation, motion.getTraj(0).vec[end_index - 1].roll);
            hand1.transform.localEulerAngles = rotation;

            var canying = GameObject.Instantiate(hand1, root1.transform);
        }
    }

    void play()
    {
        //isPlay = TrailCurveDrawCtrl.Instance().getIsPlay();
        if (isPlay)
        {
            distance += speed;
            end_index = (int)distance % ((int)motion.getTraj(cur_traj).size() - 1) + 1;
        }
    }
}
