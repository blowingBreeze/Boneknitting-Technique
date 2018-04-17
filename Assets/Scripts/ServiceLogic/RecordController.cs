using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordController
{
    private DeviceCtrl m_DeviceController;
    private FileWriter m_FileWriter;

    public RecordController()
    {
        Init();
    }

    public bool Init()
    {
        m_DeviceController = new DeviceCtrl();
        m_FileWriter = new FileWriter();
        return true;
    }

    // Update is called once per frame
    public void Update()
    {
        ModelCtrlData modelCtrlData = m_DeviceController.AcquireData();
        m_FileWriter.CacheData(modelCtrlData);
    }

    public bool InitDevice()
    {
        return m_DeviceController.InitDevice();
    }

    public void DisconnectDevice()
    {
        m_DeviceController.DisconnectDevice();
    }

    public void SaveDataToFile(MovieHeadData headData, string strFileName, float fStartTime = -1, float fEndTime = -1)
    {
        m_FileWriter.SaveDataToFile(headData, strFileName, fStartTime, fEndTime);
    }
}
