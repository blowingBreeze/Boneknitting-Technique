using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrajectoryData;

public class TrailCurveDrawCtrl
{
    //储存当前屏幕显示，即要操作的轨迹
    public HandMotion curMotion;
    //储存当前已经打开（读取）的轨迹
    //public List<HandMotion> motionList = new List<HandMotion>();

    //轨迹数量
    private int trajs_num;

    private TrailCurveDrawCtrl()
    {
        trajs_num = 2;
        curMotion = new HandMotion();
        for (int i = 0; i < trajs_num; ++i)
        {
            curMotion.Add(new Trajectory());
        }
    }
    private static TrailCurveDrawCtrl trajctrl;
    public static TrailCurveDrawCtrl Instance()
    {
        if (trajctrl == null)
            trajctrl = new TrailCurveDrawCtrl();

        return trajctrl;
    }

        /// <summary>
        /// 此接口用于设置那个轨迹要绘制哪个不绘制
        /// </summary>
        /// <param name="trailType">需要切换的轨迹类型</param>
        /// <param name="IsOn">该轨迹开启绘制还是关闭绘制</param>
    public void SwitchTrailCurve(TrailType trailType, bool IsOn)
    {
        Debug.Log(trailType + "---------" + IsOn);
        curMotion.getTraj(trailType.GetHashCode()).setActive(IsOn);
    }

    /// <summary>
    /// 接收用于绘制轨迹的参数，自行决定是否绘制轨迹每一帧调用
    /// </summary>
    /// <param name="modelCtrlData"></param>
    public void RecvTrailData(ModelCtrlData modelCtrlData)
    {
        Trajectory[] trajs = new Trajectory[trajs_num];
        for (int i = 0; i < trajs_num; ++i)
        {
            trajs[i] = new Trajectory();
        }

        Pose pt = new Pose();
        pt.time = modelCtrlData.time.ToString();
        pt.position = new Vec3(0.0f,0.0f,0.0f);
        pt.azimuth = 0.0f;
        pt.elevation = 0.0f;
        pt.roll = 0.0f;

        curMotion.getTraj(0).push_back(pt);
        curMotion.getTraj(1).push_back(pt);

    }

    //接收参数，剪切保留某一曲线上在某时间端内的轨迹
    public void ClipTrailWithinTime(float sTime, float eTime)
    {
        if (curMotion == null)
            return;

        for (int i = 0; i < curMotion.size(); ++i)
        {
            curMotion.getTraj(i).clip(sTime, eTime);
        }
    }
    //-------------轨迹图表----------------
    public List<float> speed(TrailType trailType)
    {
        return curMotion.getTraj(trailType.GetHashCode()).speed();
    }
    public List<float> acceleration(TrailType trailType)
    {
        return curMotion.getTraj(trailType.GetHashCode()).acceleration();
    }
    public List<float> curvature(TrailType trailType)
    {
        return curMotion.getTraj(trailType.GetHashCode()).curvature();
    }
    public List<float> torsion(TrailType trailType)
    {
        return curMotion.getTraj(trailType.GetHashCode()).torsion();
    }
}

public class TrailCurveAppraiseCtrl
{
    /// <summary>
    /// 接收两个轨迹数据，(用于轨迹分析)，每一帧调用
    /// </summary>
    /// <param name="refData">参考数据，此数据作为参考对象</param>
    /// <param name="AppraiseData">分析数据，需要对此数据进行分析</param>
    public void RecvCompairTrailData(ModelCtrlData refData, ModelCtrlData AppraiseData)
    {

    }

    //根据之前的分析给出评分
    public float AppraiseTrailCurve()
    {
        return 100.0f;
    }
}
