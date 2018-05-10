using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class ModelCtrlData
{
    public int frame;
    public HandCtrlData handCtrlData = new HandCtrlData();
    public BodyCtrlData bodyCtrlData = new BodyCtrlData();
    public WristCtrlData wristCtrlData = new WristCtrlData();


    public static ModelCtrlData DeepCopy(ModelCtrlData obj)
    {
        object retval;
        using (MemoryStream ms = new MemoryStream())
        {
            BinaryFormatter bf = new BinaryFormatter();
            //序列化成流
            bf.Serialize(ms, obj);
            ms.Seek(0, SeekOrigin.Begin);
            //反序列化成对象
            retval = bf.Deserialize(ms);
            ms.Close();
        }
        return (ModelCtrlData)retval;
    }

    public string toStr()
    {
        string str = string.Format("{0}\t",frame);

        str += handCtrlData.toStr();
        str += bodyCtrlData.toStr();
        str += wristCtrlData.toStr();
        str += "\n";
        return str;
    }

    public void readData(string str_data)
    {
        var data = str_data.Split('\t');
        frame = int.Parse(data[0]);
        handCtrlData.readData(data);
        bodyCtrlData.readData(data);
        wristCtrlData.readData(data);
    }
}

public class MovieHeadData
{
    public string strDoctorName;
    //头像名，从Portrait文件夹中获取头像
    public string strPortrait;
    //l录制时间 如 2018050914  （年月日时）
    public string strGenerateTime;
    //录像裁剪后的总帧数
    public int nTotalFrameCount;
    //当前帧数
    public int nCurrentFrame;
    //录像帧率
    public int nFPS;

    /// <summary>
    /// 将MovieHeadData转换成一个用'\t'分隔的字符串
    /// </summary>
    /// <returns></returns>
    public string toStr()
    {
        return string.Format("MOVIE_DATA\t{0}\t{1}\t{2}\t{3:D}\t{4:D}\n", strDoctorName, strPortrait, strGenerateTime, nTotalFrameCount, nFPS);
    }

    /// <summary>
    /// 使用一个字符串来初始化MovieHeadData
    /// </summary>
    /// <param name="str"></param>
    public void ReadData(string str)
    {
        string[] temp = str.Split('\t');//该数组第一位是数据类型标志位，所以从有用数据从下标1开始
        strDoctorName = temp[1];
        strPortrait = temp[2];
        strGenerateTime=temp[3];
        nTotalFrameCount = int.Parse(temp[4]);
        nFPS = int.Parse(temp[5]);
    }

    public MovieHeadData()
    {

    }

    /// <summary>
    /// 使用一个字符串来构造MovieHeadData
    /// </summary>
    /// <param name="str"></param>
    public MovieHeadData(string str)
    {
        ReadData(str);
    }
}

public class VideoRateCtrl
{
    private int m_nTotalFrameCount;
    public int nTotalFrameCount
    {
        get { return m_nTotalFrameCount; }
        set { m_nTotalFrameCount = value; }
    }

    private int m_nCurrentFrame;    //当前帧
    public int nCurrentFrame
    {
        set
        {
            if(value>m_nTotalFrameCount)
            {
                m_nCurrentFrame = m_nTotalFrameCount;
            }
            else
            {
                m_nCurrentFrame = value;
            }
        }
        get
        {
            return m_nCurrentFrame;
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

    public VideoRateCtrl(int nTotalFrameCount, float fIntervalTime, int nCurrentFrame= 0)
    {
        InitVideoRateCtrl(nTotalFrameCount, fIntervalTime, nCurrentFrame);
    }

    public bool InitVideoRateCtrl(int nTotalFrameCount, float fIntervalTime, int nCurrentFrame = 0)
    {
        m_nTotalFrameCount = nTotalFrameCount;
        m_nCurrentFrame = nCurrentFrame;
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

    public WristCtrlData()
    {
        left_wrist_rotate = new Vector3();
        right_wrist_rotate = new Vector3();
    }

    public WristCtrlData(WristCtrlData data)
    {
        left_wrist_rotate = data.left_wrist_rotate;
        right_wrist_rotate = data.right_wrist_rotate;
    }
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