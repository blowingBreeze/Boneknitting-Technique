using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCtrlData
{

    Quaternion[] jointRotation;
    Vector3 UserPosition;
}

public class MovieHeadData
{
    public string strDoctorName;
    //头像路径，用来读取头像放在列表中
    public string strPortraitPath;

    public float fTotalTime;
    public float fIntervalTime;
}

public class VideoRateCtrl
{
    public float fTotalTime;
    public float fCurrentTime;    //当前播放时刻
    public float fIntervalTime;   //当前播放时间间隔
}

public enum TrailType
{
    EG_S1,
    EG_S2,
    EG_S3,
    EG_S4
}