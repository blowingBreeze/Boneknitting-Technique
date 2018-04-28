using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceCtrl
{
    
    //初始化设备
    public bool InitDevice()
    {
        DTGloveManager.instance.InitDevice();
        return true;
    }

    //断开设备连接
    public void DisconnectDevice()
    {
        DTGloveManager.instance.DisconnectDevice();
    }

    //从设备读取当前帧数据
    public ModelCtrlData AcquireData()
    {
        DTGloveManager.instance.AcquireHandData();
        return new ModelCtrlData();
    }
}
