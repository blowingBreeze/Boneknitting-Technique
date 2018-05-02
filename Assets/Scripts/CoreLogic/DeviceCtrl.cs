using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceCtrl
{
    // private instance of the KinectManager
    protected KinectManager kinectManager;

    //初始化设备
    public bool InitDevice()
    {
        // Get the KinectManager instance
        if (kinectManager == null)
        {
            kinectManager = KinectManager.Instance;
        }

        return true;
    }

    //断开设备连接
    public void DisconnectDevice()
    {

    }

    //从设备读取当前帧数据
    public ModelCtrlData AcquireData()
    {
        return new ModelCtrlData();
    }
}
