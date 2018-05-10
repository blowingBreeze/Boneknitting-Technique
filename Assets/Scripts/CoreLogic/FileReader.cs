using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileConfig
{
    public const int KINECT_NODE_NUM = 22;
    public const int FIVE_DT_NODE_NUM = 14;
}

public class FileReader
{
    private string m_filepath;
    public MovieHeadData m_head_data;
    public List<ModelCtrlData> m_data_list = new List<ModelCtrlData>();

    //用文件路径初始化对象
    public FileReader(string strFilePath)
    {
        m_filepath = strFilePath;
        m_head_data = GetHeadFromFile(strFilePath);
        readTxtFile(m_filepath,ref m_data_list);
    }

    /// <summary>
    /// 获取文件中某个时间点的数据 （也是每帧调用）
    /// </summary>
    /// <param name="fTime">时间点，单位毫秒</param>
    /// <returns></returns>
    public ModelCtrlData PraseDataByTime(int nFrameCount)
    {
        if (nFrameCount < 0) nFrameCount = 0;
        if (nFrameCount > m_head_data.nTotalFrameCount - 1) nFrameCount = m_head_data.nTotalFrameCount - 1;
        return m_data_list[nFrameCount];
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

                while ((line = sr.ReadLine()) != null)
                {
                    ModelCtrlData frame = new ModelCtrlData();
                    frame.readData(line);
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
        string line = "";

        try
        {
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                line = sr.ReadLine();
                var temp = line.Split('\t');
                if (temp[0] != "MOVIE_DATA")
                    Debug.Log("The file is not a movie data!");

                sr.Close();
            }
        }
        catch (Exception e)
        {
            Debug.Log("The file could not be read:");
            Debug.Log(e.Message);
            line = "MOVIE_DATA/tFILE_ERROR";
        }


        return new MovieHeadData(line);
    }
}