using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileReader
{
    //用文件路径初始化对象
    public FileReader(string strFilePath=null)
    {

    }

    //获取某个时间点的数据 （也是每帧调用）
    public ModelCtrlData PraseDataByTime(float fTime)
    {
        return new ModelCtrlData();
    }

    //根据文件路径读取文件头
    public static MovieHeadData GetHeadFromFile(string strFilePath)
    {
        var tempMovieHead = new MovieHeadData();
        tempMovieHead.fCurrentTime = 1;
        tempMovieHead.fIntervalTime = 1;
        tempMovieHead.fTotalTime = 1000;
        tempMovieHead.strDoctorName = "李医生";
        return tempMovieHead;
    }
}
