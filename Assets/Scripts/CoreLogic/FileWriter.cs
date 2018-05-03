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
        cacheDataList.Add(modelCtrlData);
    }

    /// <summary>
    /// 从缓存的数据中获取某个时刻的模型控制数据
    /// </summary>
    /// <param name="fTime">传入时刻，单位ms</param>
    /// <returns></returns>
    public ModelCtrlData GetModelCtrlDataByTime(float fTime)
    {
        
        fTime = Math.Abs(fTime);
        if (fTime > 1.0f)
            fTime = fTime - (float)Math.Ceiling(fTime) + 1;

        int index = (int)((cacheDataList.Count - 1) * fTime);
        return cacheDataList[index];
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

        int start_index = (int)(start * cacheDataList.Count);
        int end_index = (int)(end * (cacheDataList.Count-1));

        try
        {
            using (StreamWriter sw = new StreamWriter(strFileName, false))
            {
                //文件头
                sw.Write("MOVIE_DATA\t");
                sw.Write("{0}\t", headData.fCurrentTime);
                sw.Write("{0}\t", headData.fIntervalTime);
                sw.Write("{0}\t", headData.fTotalTime);
                sw.Write("{0}\t", headData.strDoctorName);
                sw.Write("{0}\t", headData.strPortraitPath);
                sw.Write("\n");

                //数据
                for (int i = start_index; i < end_index; ++i)
                {
                    sw.Write("{0}\t", cacheDataList[i].time);
                    for (int j = 0; j < FileConfig.FIVE_DT_NODE_NUM; ++j)
                    {
                        sw.Write("{0}\t", cacheDataList[i].HandData[j]);
                    }
                    for (int j = 0; j < FileConfig.KINECT_NODE_NUM; ++j)
                    {
                        sw.Write("{0}\t{1}\t{2}\t{3}\t",
                            cacheDataList[i].jointRotation[j].w,
                            cacheDataList[i].jointRotation[j].x,
                            cacheDataList[i].jointRotation[j].y,
                            cacheDataList[i].jointRotation[j].z);
                    }
                    sw.Write("{0}\t{1}\t{2}\t",
                        cacheDataList[i].left_wrist_rotate.x,
                        cacheDataList[i].left_wrist_rotate.y,
                        cacheDataList[i].left_wrist_rotate.z);
                    sw.Write("{0}\t{1}\t{2}\t",
                        cacheDataList[i].right_wrist_rotate.x,
                        cacheDataList[i].right_wrist_rotate.y,
                        cacheDataList[i].right_wrist_rotate.z);
                    sw.Write("{0}\t{1}\t{2}\t",
                        cacheDataList[i].UserPosition.x,
                        cacheDataList[i].UserPosition.y,
                        cacheDataList[i].UserPosition.z);

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
