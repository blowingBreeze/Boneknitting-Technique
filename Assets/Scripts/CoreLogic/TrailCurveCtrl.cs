using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCurveCtrl
{
    //此接口用于设置那个轨迹要绘制哪个不绘制
    public void SwitchTrailCurve(TrailType trailType, bool IsOn)
    {
        Debug.Log(trailType + "---------" + IsOn);
    }

    //接收用于绘制轨迹的参数，自行决定是否绘制轨迹每一帧调用
    public void RecvTrailData(ModelCtrlData modelCtrlData)
    {

    }

    //接收参数，删除某一曲线上在某个时间点之后的轨迹
    public void DeleteTrailAfterTime(float fTime)
    {

    }

    //接收两个轨迹数据，(用于轨迹分析)，每一帧调用
    public void RecvCompairTrailData(ModelCtrlData refData, ModelCtrlData AppraiseData)
    {

    }
    //根据之前的分析给出评分
    public float AppraiseTrailCurve()
    {
        return 100.0f;
    }

}
