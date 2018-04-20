using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCurveDrawCtrl
{
   
        /// <summary>
        /// 此接口用于设置那个轨迹要绘制哪个不绘制
        /// </summary>
        /// <param name="trailType">需要切换的轨迹类型</param>
        /// <param name="IsOn">该轨迹开启绘制还是关闭绘制</param>
    public void SwitchTrailCurve(TrailType trailType, bool IsOn)
    {
        Debug.Log(trailType + "---------" + IsOn);
    }

    /// <summary>
    /// 接收用于绘制轨迹的参数，自行决定是否绘制轨迹每一帧调用
    /// </summary>
    /// <param name="modelCtrlData"></param>
    public void RecvTrailData(ModelCtrlData modelCtrlData)
    {

    }

    //接收参数，删除某一曲线上在某个时间点之后的轨迹
    public void DeleteTrailAfterTime(float fTime)
    {

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
