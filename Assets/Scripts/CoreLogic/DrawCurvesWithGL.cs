using System.Collections.Generic;
using TrajectoryData;
using UnityEngine;

public class DrawCurvesWithGL : MonoBehaviour
{
    // When added to an object, draws colored rays from the
    // transform position.
    private int start_index = 0;
    private int end_index = 0;
    private HandMotion motion = new HandMotion();
    private int cur_traj = 0;
    private GameObject hand;
    private List<float> color_list;

    private float speed = 0.5F;
    private float distance = 1.0F;
    private bool isPlay = false;

    static Material lineMaterial;
    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    void OnEnable()
    {
        motion = TrailCurveDrawCtrl.Instance().curMotion;
        hand = new GameObject();

        start_index = 0;
        end_index = (int)motion.getTraj(cur_traj).size();

        color_list = TrailCurveAppraiseCtrl.color_list;
    }

    void Start()
    {
    }

    void Update()
    {
        //drawHand();
    }
    // Will be called after all regular rendering is done
    public void OnRenderObject()
    {
        //isPlay = TrailCurveDrawCtrl.Instance().getIsPlay();
        draw();
    }
    void drawHand()
    {
        Vector3 rotation = transform.localEulerAngles;
        hand.transform.localPosition = new Vector3(motion.getTraj(0).vec[end_index - 1].position.x, motion.getTraj(0).vec[end_index - 1].position.y, motion.getTraj(0).vec[end_index - 1].position.z);
        rotation = new Vector3(motion.getTraj(0).vec[end_index - 1].azimuth, motion.getTraj(0).vec[end_index - 1].elevation, motion.getTraj(0).vec[end_index - 1].roll);
        hand.transform.localEulerAngles = rotation;
    }
    void play()
    {
        if (isPlay)
        {
            distance += speed;
            end_index = (int)distance % ((int)motion.getTraj(cur_traj).size() - 1) + 1;
        }
    }

    void draw()
    {
        play();

        CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);

        GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
        GL.MultMatrix(transform.localToWorldMatrix);
        // Draw lines
        GL.Begin(GL.LINE_STRIP);

        for (int i = 0; i < motion.size(); ++i)
        {
            drawCurve(motion.getTraj(i));
        }

        GL.End();
        GL.PopMatrix();
    }
    void drawCurve(Trajectory traj)
    {
        Vec3 temp = new Vec3();
        GL.Color(Color.white);
        for (int j = start_index; j < end_index; ++j)
        {
            temp = traj.vec[j].position;
            GL.Vertex3(temp.x, temp.y, temp.z);
        }
    }

    void drawCurveWithColorList(Trajectory traj, List<float> color_list)
    {
        GL.Color(Color.red);
        Vec3 temp = new Vec3();
        for (int j = start_index; j < end_index; ++j)
        {
            temp = traj.vec[j].position;
            GL.Color(new Color(color_list[j] * 2, 1 - color_list[j] * 2, 0, 1.0F));
            GL.Vertex3(temp.x, temp.y, temp.z);
        }
    }
}
