using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCtrlData
{
    public float time;
    public float[] HandData = new float[14];
    public Quaternion[] jointRotation = new Quaternion[22];
    public Vector3 left_wrist_rotate;
    public Vector3 right_wrist_rotate;
    public Vector3 UserPosition;
}

public class MovieHeadData
{
    public string strDoctorName;
    //头像路径，用来读取头像放在列表中
    public string strPortraitPath;

    //录像裁剪后的时长
    public float fTotalTime;
    //录像帧间隔时间
    public float fIntervalTime;
    //录像当前已播放时间
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
                m_fCurrentTime = m_fTotalTime;
            }
            else
            {
                m_fCurrentTime = value;
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
            if(value /m_fDefaultIntervalTime>20.0f)
            {
                m_fIntervalTime = 20.0f*m_fDefaultIntervalTime;
            }
            else if(value / m_fDefaultIntervalTime < 0.05f)
            {
                m_fIntervalTime = 0.05f * m_fDefaultIntervalTime;
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

    private float m_fDefaultIntervalTime;
    public float GetAccelerate()
    {
        return m_fIntervalTime / m_fDefaultIntervalTime;
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
        m_fDefaultIntervalTime = fIntervalTime;
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
