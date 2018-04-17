using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordController
{
    private DeviceCtrl m_DeviceController;
    private FileWriter m_FileWriter;
    private PlayController m_PlayController;

    public RecordController(GameObject model)
    {
        Init(model);
    }

    public bool Init(GameObject model)
    {
        m_DeviceController = new DeviceCtrl();
        m_FileWriter = new FileWriter();
        m_PlayController = new PlayController(model);
        return true;
    }

    // Update is called once per frame
    public void Update()
    {
        ModelCtrlData modelCtrlData = m_DeviceController.AcquireData();
        m_FileWriter.CacheData(modelCtrlData);
        m_PlayController.Update(modelCtrlData);
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

    public PlayController GetPlayController()
    {
        return m_PlayController;
    }

    public ModelCtrlData GetModelCtrlDataByTime(float fTime)
    {
        return m_FileWriter.GetModelCtrlDataByTime(fTime);
    }
}
