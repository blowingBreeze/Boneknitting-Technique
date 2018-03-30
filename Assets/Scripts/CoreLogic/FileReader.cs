using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileReader
{
    //用文件路径初始化对象
    public FileReader(string strFilePath=null)
    {

    }

    //根据文件路径读取文件头
    public MovieHeadData GetHeadFromFile(string strFilePath)
    {
        return new MovieHeadData();
    }

    //获取某个时间点的数据 （也是每帧调用）
    public ModelCtrlData PraseDataByTime(float fTime)
    {
        return new ModelCtrlData();
    }


}
