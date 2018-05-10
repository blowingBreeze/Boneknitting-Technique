using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class FileWriter
{
    private List<ModelCtrlData> cacheDataList = new List<ModelCtrlData>();
    //此接口用于缓存每一帧传来的数据，（设备传来的数据）
    public void CacheData(ModelCtrlData modelCtrlData)
    {
        ModelCtrlData temp = ModelCtrlData.DeepCopy(modelCtrlData);
        cacheDataList.Add(temp);
    }

    /// <summary>
    /// 从缓存的数据中获取某个时刻的模型控制数据
    /// </summary>
    /// <param name="fTime">传入时刻，单位ms</param>
    /// <returns></returns>
    public ModelCtrlData GetModelCtrlDataByTime(int frame)
    {

        if (frame < 0) frame = 0;
        if (frame > cacheDataList.Count - 1) frame = cacheDataList.Count - 1;

        return cacheDataList[frame];
    }

    /// <summary>
    /// 将模型数据写入某个文件中,其中时间是裁剪后的时间点默认不裁剪
    /// </summary>
    /// <param name="headData">需要写入文件的信息头</param>
    /// <param name="strFileName">文件存储全路径</param>
    /// <param name="fStartTime">裁剪前录像裁剪前端时间点，毫秒为单位</param>
    /// <param name="fEndTime">裁剪前录像裁剪后端时间点，毫秒为单位</param>
    public void SaveDataToFile(MovieHeadData headData, string strFileName, float start = -1, float end = -1)
    {

        if (cacheDataList.Count <= 0 || end <= start) return;
        if (end <= 0.0F) return;
        if (start <= 0.0F) start = 0.0F;
        if (end >= 1.0F) end = 1.0F;

        int start_index = (int)(start * (cacheDataList.Count-1));
        int end_index = (int)(end * (cacheDataList.Count-1));

        try
        {
            using (StreamWriter sw = new StreamWriter(strFileName, false))
            {
                //文件头
                sw.Write(headData.toStr());

                //数据
                for (int i = start_index; i < end_index; ++i)
                {
                    //时间
                    sw.Write("{0}\t", i);
                    //5DT数据
                    for (int j = 0; j < FileConfig.FIVE_DT_NODE_NUM; ++j)
                    {
                        sw.Write("{0}\t", cacheDataList[i].handCtrlData.HandData[j]);
                    }
                    //kinect数据
                    for (int j = 0; j < FileConfig.KINECT_NODE_NUM; ++j)
                    {
                        sw.Write("{0}\t{1}\t{2}\t{3}\t",
                            cacheDataList[i].bodyCtrlData.jointRotation[j].w,
                            cacheDataList[i].bodyCtrlData.jointRotation[j].x,
                            cacheDataList[i].bodyCtrlData.jointRotation[j].y,
                            cacheDataList[i].bodyCtrlData.jointRotation[j].z);
                    }
                    //模型整体位置
                    sw.Write("{0}\t{1}\t{2}\t",
                        cacheDataList[i].bodyCtrlData.userPosition.x,
                        cacheDataList[i].bodyCtrlData.userPosition.y,
                        cacheDataList[i].bodyCtrlData.userPosition.z);
                    //左右手腕位置
                    sw.Write("{0}\t{1}\t{2}\t",
                        cacheDataList[i].bodyCtrlData.HandLeftPos.x,
                        cacheDataList[i].bodyCtrlData.HandLeftPos.y,
                        cacheDataList[i].bodyCtrlData.HandLeftPos.z);
                    sw.Write("{0}\t{1}\t{2}\t",
                        cacheDataList[i].bodyCtrlData.HandRightPos.x,
                        cacheDataList[i].bodyCtrlData.HandRightPos.y,
                        cacheDataList[i].bodyCtrlData.HandRightPos.z);
                    //用户ID
                    sw.Write("{0}\t", cacheDataList[i].bodyCtrlData.UserID);
                    //左右手腕旋转量
                    sw.Write(cacheDataList[i].wristCtrlData.toStr());

                    sw.Write("\n");
                }

                sw.Close();
            }
        }
        catch (Exception e)
        {
            Debug.Log("The file could not be write:");
            Debug.Log(e);
        }
    }

}
