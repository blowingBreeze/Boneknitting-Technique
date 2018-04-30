using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceCtrl
{
    //初始化设备
    public bool InitDevice()
    {
        return true;
    }

    //断开设备连接
    public void DisconnectDevice()
    {

    }

    /// <summary>
    /// 从各个设备读取当前帧数据，整合成为当前帧可以用于控制模型的数据
    /// </summary>
    /// <returns></returns>
    public ModelCtrlData AcquireData()
    {
        return new ModelCtrlData();
    }
}
