using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordController
{
    private float fInterval;
    //private DeviceCtrl m_DeviceController;
    //private ModelCtrl m_ModelController;
    private FileWriter m_FileWriter;

    public RecordController()
    {

    }

    public bool Init()
    {
        return true;
    }

    // Update is called once per frame
    public void Update()
    {
         //此处添加录制逻辑
    }

    public bool InitDevice()
    {
        return true;
    }
}
