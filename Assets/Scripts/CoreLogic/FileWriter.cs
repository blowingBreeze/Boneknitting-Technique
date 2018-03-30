using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileWriter
{

    //此接口用于缓存每一帧传来的数据，（设备传来的数据）
    public void CacheData(ModelCtrlData modelCtrlData)
    {

    }

    //将模型数据写入某个文件中,其中时间是裁剪后的时间点默认不裁剪
    public void SaveDataToFile(MovieHeadData headData, string strFileName, float fStartTime = -1, float fEndTime = -1)
    {

    }

}
