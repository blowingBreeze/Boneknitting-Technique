using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileConfig
{
    public static const int KINECT_NODE_NUM = 22;
    public static const int FIVE_DT_NODE_NUM = 14;
}

public class FileReader
{
    private string m_filepath;
    public List<ModelCtrlData> m_data_list = new List<ModelCtrlData>();

    //用文件路径初始化对象
    public FileReader(string strFilePath)
    {
        m_filepath = strFilePath;
        readTxtFile(m_filepath,ref m_data_list);
    }

    /// <summary>
    /// 获取文件中某个时间点的数据 （也是每帧调用）
    /// </summary>
    /// <param name="fTime">时间点，单位毫秒</param>
    /// <returns></returns>
    public ModelCtrlData PraseDataByTime(float fTime)
    {
        fTime = Math.Abs(fTime);
        if(fTime>1.0f)
            fTime = fTime - (float)Math.Ceiling(fTime) + 1;

        int index = (int)((m_data_list.Count - 1) * fTime);
        return m_data_list[index];
    }

    public static bool readTxtFile(string path, ref List<ModelCtrlData> target)
    {
        string line = "";

        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                line = sr.ReadLine();
                var temp = line.Split('\t');
                if (temp[0] != "MOVIE_DATA")
                    return false;

                line = sr.ReadLine();
                temp = line.Split('\t');
                if (temp[0] != "time")
                    return false;

                ModelCtrlData frame = new ModelCtrlData();
                while ((line = sr.ReadLine()) != null)
                {
                    temp = line.Split('\t');
                    frame.time = float.Parse(temp[0]);

                    int start_index = 1;

                    for (int i = start_index; i < FileConfig.FIVE_DT_NODE_NUM + start_index; ++i)
                    {
                        frame.HandData[i] = float.Parse(temp[i]);
                    }

                    start_index = FileConfig.FIVE_DT_NODE_NUM + start_index;

                    for (int i = 0; i < FileConfig.KINECT_NODE_NUM; ++i)
                    {
                        frame.jointRotation[i].w = float.Parse(temp[start_index + i * 4]);
                        frame.jointRotation[i].x = float.Parse(temp[start_index + i * 4 + 1]);
                        frame.jointRotation[i].y = float.Parse(temp[start_index + i * 4 + 2]);
                        frame.jointRotation[i].z = float.Parse(temp[start_index + i * 4 + 3]);
                    }

                    start_index = start_index + FileConfig.KINECT_NODE_NUM;

                    frame.left_wrist_rotate.x = float.Parse(temp[start_index]);
                    frame.left_wrist_rotate.y = float.Parse(temp[start_index + 1]);
                    frame.left_wrist_rotate.z = float.Parse(temp[start_index + 2]);
                    frame.right_wrist_rotate.x = float.Parse(temp[start_index + 3]);
                    frame.right_wrist_rotate.y = float.Parse(temp[start_index + 4]);
                    frame.right_wrist_rotate.z = float.Parse(temp[start_index + 5]);
                    frame.UserPosition.x = float.Parse(temp[start_index + 6]);
                    frame.UserPosition.y = float.Parse(temp[start_index + 7]);
                    frame.UserPosition.z = float.Parse(temp[start_index + 8]);

                    target.Add(frame);
                }

                sr.Close();
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.Log("The file could not be read:");
            Debug.Log(e.Message);
            return false;
        }

    }
    /// <summary>
    /// 根据文件路径获取文件头，此为静态函数
    /// </summary>
    /// <param name="strFilePath"></param>
    /// <returns></returns>
    public static MovieHeadData GetHeadFromFile(string strFilePath)
    {
        var tempMovieHead = new MovieHeadData();

        string line = "";

        try
        {
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                line = sr.ReadLine();
                var temp = line.Split('\t');
                if (temp[0] != "MOVIE_DATA")
                    Debug.Log("The file could not be read:");

                tempMovieHead.fCurrentTime = float.Parse(temp[1]);
                tempMovieHead.fIntervalTime = float.Parse(temp[2]);
                tempMovieHead.fTotalTime = float.Parse(temp[3]);
                tempMovieHead.strDoctorName = temp[4];
                tempMovieHead.strPortraitPath = temp[5];

                sr.Close();
            }
        }
        catch (Exception e)
        {
            Debug.Log("The file could not be read:");
            Debug.Log(e.Message);
        }

        return tempMovieHead;
    }
}