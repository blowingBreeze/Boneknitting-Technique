using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager:MonoBehaviour
{
    private RecordController m_RecordController;
    private bool bIsStartRecord;
    private float fTimeCount;
    private FileWriter m_FileWriter;

    private GameObject HumenModel;

    private void Awake()
    {
        m_RecordController = new RecordController(HumenModel);
        bIsStartRecord = false;
        fTimeCount = 0.0f;
        m_FileWriter = new FileWriter();
    }

    private void Update()
    {
        if (bIsStartRecord)
        {
            fTimeCount += Time.deltaTime * 1000;
            m_RecordController.Update();
            ModelCtrlData modelCtrlData = m_RecordController.GetCurrentFrameData();
            m_FileWriter.CacheData(modelCtrlData);
        }
    }

    public RecordController GetRecordController()
    {
        return m_RecordController;
    }

    public bool InitDevice()
    {
        return m_RecordController.InitDevice();
    }

    public void DisconnnectDevice()
    {
        m_RecordController.DisconnectDevice();
    }

    public bool IsStartRecord()
    {
        return bIsStartRecord;
    }

    public void StartOrStopRecord()
    {
        bIsStartRecord = !bIsStartRecord;
    }

    public void SaveDataToFile(MovieHeadData headData, string strFileName, float fStartTime = -1, float fEndTime = -1)
    {
        m_FileWriter.SaveDataToFile(headData, strFileName, fStartTime, fEndTime);
    }

    public ModelCtrlData GetModelCtrlDataByTime(float fTime)
    {
        return m_FileWriter.GetModelCtrlDataByTime(fTime);
    }

    /// <summary>
    /// 返回一个以毫秒为单位的录制时间长度
    /// </summary>
    /// <returns></returns>
    public float GetTimeCount()
    {
        return fTimeCount;
    }
}
