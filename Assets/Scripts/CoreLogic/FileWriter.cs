using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileWriter
{

    //此接口用于缓存每一帧传来的数据，（设备传来的数据）
    public void CacheData(ModelCtrlData modelCtrlData)
    {

    }

    /// <summary>
    /// 从缓存的数据中获取某个时刻的模型控制数据
    /// </summary>
    /// <param name="fTime">传入时刻，单位ms</param>
    /// <returns></returns>
    public ModelCtrlData GetModelCtrlDataByTime(float fTime)
    {
        return new ModelCtrlData();
    }

    /// <summary>
    /// 将模型数据写入某个文件中,其中时间是裁剪后的时间点默认不裁剪
    /// </summary>
    /// <param name="headData">需要写入文件的信息头</param>
    /// <param name="strFileName">文件存储全路径</param>
    /// <param name="fStartTime">裁剪前录像裁剪前端时间点，毫秒为单位</param>
    /// <param name="fEndTime">裁剪前录像裁剪后端时间点，毫秒为单位</param>
    public void SaveDataToFile(MovieHeadData headData, string strFileName, float fStartTime = -1, float fEndTime = -1)
    {

    }

}
