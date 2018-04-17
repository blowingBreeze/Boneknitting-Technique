using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager:MonoBehaviour
{
    private RecordController m_RecordController;
    private bool bIsStartRecord;
    private float fTimeCount;
    private GameObject HumenModel;

    private void Awake()
    {
        m_RecordController = new RecordController(HumenModel);
        bIsStartRecord = false;
        fTimeCount = 0.0f;
    }

    private void Update()
    {
        if (bIsStartRecord)
        {
            m_RecordController.Update();
            fTimeCount += Time.deltaTime * 1000;
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

    /// <summary>
    /// 返回一个以毫秒为单位的录制时间长度
    /// </summary>
    /// <returns></returns>
    public float GetTimeCount()
    {
        return fTimeCount;
    }
}
