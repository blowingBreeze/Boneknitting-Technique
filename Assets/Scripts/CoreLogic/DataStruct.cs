using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCtrlData
{
    public float time;
    public HandCtrlData handCtrlData = new HandCtrlData();
    public BodyCtrlData bodyCtrlData = new BodyCtrlData();
    public WristCtrlData wristCtrlData = new WristCtrlData();

    public string toStr()
    {
        string str = string.Format("{0}\t",time);

        str += handCtrlData.toStr();
        str += bodyCtrlData.toStr();
        str += wristCtrlData.toStr();
        str += "\n";
        return str;
    }

    public void readData(string[] data)
    {
        time = float.Parse(data[0]);
        handCtrlData.readData(data);
        bodyCtrlData.readData(data);
        wristCtrlData.readData(data);
    }
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

public enum ChartType
{
    CHART_SPEED,
    CHART_ACCELERATE,
    CHART_CURVATURE,
    CHART_TORSION
}

public class HandCtrlData
{
    public float[] HandData=new float[14];

    public string toStr()
    {
        string str = "";

        for (int i = 0; i < FileConfig.FIVE_DT_NODE_NUM; ++i)
        {
            str += string.Format("{0}\t", HandData[i]);
        }

        return str;
    }
    public void readData(string[] data)
    {
        int start_index = 1;
        for (int i = start_index; i < FileConfig.FIVE_DT_NODE_NUM + start_index; ++i)
        {
            HandData[i] = float.Parse(data[i]);
        }
    }
}
public class BodyCtrlData
{
    public Quaternion[] jointRotation = new Quaternion[22];
    public Vector3 userPosition;
    public Vector3 HandLeftPos;
    public Vector3 HandRightPos;
    public uint UserID;
    public string toStr()
    {
        string str = "";

        for (int i = 0; i < FileConfig.KINECT_NODE_NUM; ++i)
        {
            str += string.Format("{0}\t{1}\t{2}\t{3}\t",
                                        jointRotation[i].x,
                                        jointRotation[i].y,
                                        jointRotation[i].z,
                                        jointRotation[i].w);
        }

        str += string.Format("{0}\t{1}\t{2}\t",
                                        userPosition.x,
                                        userPosition.y,
                                        userPosition.z);

        str += string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t",
                        HandLeftPos.x,
                        HandLeftPos.y,
                        HandLeftPos.z,
                        HandRightPos.x,
                        HandRightPos.y,
                        HandRightPos.z);

        str += string.Format("{0}\t", UserID);

        return str;
    }
    public void readData(string[] data)
    {
        int start_index = 1 + FileConfig.FIVE_DT_NODE_NUM;
        //骨节点
        for (int i = 0; i < FileConfig.KINECT_NODE_NUM; ++i)
        {
            jointRotation[i].w = float.Parse(data[start_index + i * 4]);
            jointRotation[i].x = float.Parse(data[start_index + i * 4 + 1]);
            jointRotation[i].y = float.Parse(data[start_index + i * 4 + 2]);
            jointRotation[i].z = float.Parse(data[start_index + i * 4 + 3]);
        }

        start_index = start_index + FileConfig.KINECT_NODE_NUM * 4;

        //模型整体位置
        userPosition.x = float.Parse(data[start_index]);
        userPosition.y = float.Parse(data[start_index + 1]);
        userPosition.z = float.Parse(data[start_index + 2]);

        start_index = start_index + 3;
        //左右手腕位置
        userPosition.x = float.Parse(data[start_index]);
        userPosition.y = float.Parse(data[start_index + 1]);
        userPosition.z = float.Parse(data[start_index + 2]);
        userPosition.x = float.Parse(data[start_index + 3]);
        userPosition.y = float.Parse(data[start_index + 4]);
        userPosition.z = float.Parse(data[start_index + 5]);

        start_index = start_index + 6;

        //用户ID
        UserID = uint.Parse(data[start_index]);
    }
}
public class WristCtrlData
{
    public Vector3 left_wrist_rotate;
    public Vector3 right_wrist_rotate;
    //public Vector3 left_wrist_accelerometer;
    //public Vector3 right_wrist_accelerometer;
    //public Vector3 left_wrist_angular_velocity;
    //public Vector3 right_wrist_angular_velocity;
    public string toStr()
    {
        string str = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t",
                        left_wrist_rotate.x,
                        left_wrist_rotate.y,
                        left_wrist_rotate.z,
                        right_wrist_rotate.x,
                        right_wrist_rotate.y,
                        right_wrist_rotate.z);

        return str;
    }
    public void readData(string[] data)
    {
        int start_index = 1 + FileConfig.FIVE_DT_NODE_NUM + FileConfig.KINECT_NODE_NUM * 4 + 3 + 6 + 1;
        //左右手腕旋转量
        left_wrist_rotate.x = float.Parse(data[start_index]);
        left_wrist_rotate.y = float.Parse(data[start_index + 1]);
        left_wrist_rotate.z = float.Parse(data[start_index + 2]);
        right_wrist_rotate.x = float.Parse(data[start_index + 3]);
        right_wrist_rotate.y = float.Parse(data[start_index + 4]);
        right_wrist_rotate.z = float.Parse(data[start_index + 5]);
    }
}