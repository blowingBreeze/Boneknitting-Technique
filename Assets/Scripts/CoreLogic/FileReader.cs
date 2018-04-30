using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileReader
{
    //用文件路径初始化对象
    public FileReader(string strFilePath)
    {

    }

    /// <summary>
    /// 获取文件中某个时间点的数据 （也是每帧调用）
    /// </summary>
    /// <param name="fTime">时间点，单位毫秒</param>
    /// <returns></returns>
    public ModelCtrlData PraseDataByTime(float fTime)
    {
        return new ModelCtrlData();
    }

    /// <summary>
    /// 根据文件路径获取文件头，此为静态函数
    /// </summary>
    /// <param name="strFilePath"></param>
    /// <returns></returns>
    public static MovieHeadData GetHeadFromFile(string strFilePath)
    {
        var tempMovieHead = new MovieHeadData();
        tempMovieHead.fCurrentTime = 1;
        tempMovieHead.fIntervalTime = 100;
        tempMovieHead.fTotalTime = 100000;
        tempMovieHead.strDoctorName = "李医生";
        tempMovieHead.strPortraitPath = "";
        return tempMovieHead;
    }
}
