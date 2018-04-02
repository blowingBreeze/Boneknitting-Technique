using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCtrlData
{


}

public class MovieHeadData
{
    public string strDoctorName;
    //头像路径，用来读取头像放在列表中
    public string strPortraitPath;

    public float fTotalTime;
    public float fIntervalTime;
    public float fCurrentTime;
}

public class VideoRateCtrl
{
    private float m_fTotalTime;
    public float fTotalTime
    {
        get { return m_fTotalTime; }
    }

    private float m_fCurrentTime;    //当前播放时刻
    public float fCurrentTime
    {
        set
        {
            if(value>m_fTotalTime)
            {
                m_fIntervalTime = m_fTotalTime;
            }
            else
            {
                m_fIntervalTime = value;
            }
        }
        get
        {
            return m_fCurrentTime;
        }
    }

    private float m_fIntervalTime;   //当前播放时间间隔
    public float fIntervalTime
    {
        set
        {
            if(value>0.04)
            {
                m_fIntervalTime = 0.04f;
            }
            else
            {
                m_fIntervalTime = value;
            }
        }
        get
        {
            return m_fIntervalTime;
        }
    }

    public VideoRateCtrl(float fTotalTime, float fIntervalTime, float fCurrentTime = 0f)
    {
        Init(fTotalTime, fIntervalTime, fCurrentTime);
    }

    public bool Init(float fTotalTime, float fIntervalTime, float fCurrentTime = 0f)
    {
        m_fTotalTime = fTotalTime;
        m_fCurrentTime = fCurrentTime;
        m_fIntervalTime = fIntervalTime;
        return true;
    }
}

public enum TrailType
{
    EG_S1,
    EG_S2,
    EG_S3,
    EG_S4
}