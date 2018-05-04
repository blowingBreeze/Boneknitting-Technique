using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceCtrl
{

    private ModelCtrlData cur_ModelCtrlData=new ModelCtrlData();
    // private instance of the KinectManager
    protected KinectManager kinectManager;
    protected DTGloveManager gloveManager;

    //初始化设备
    public bool InitDevice()
    {
        // Get the KinectManager instance
        if (kinectManager == null)
        {
            kinectManager = KinectManager.Instance;
        }

        if (gloveManager == null)
        {
            gloveManager = DTGloveManager.instance;
        }

        return true;

    }

    //断开设备连接
    public void DisconnectDevice()
    {
        gloveManager.DisconnectDevice();
    }

    /// <summary>
    /// 从各个设备读取当前帧数据，整合成为当前帧可以用于控制模型的数据
    /// </summary>
    /// <returns></returns>
    public ModelCtrlData AcquireData()
    {
        gloveManager.AcquireHandData(ref cur_ModelCtrlData.handCtrlData);
        cur_ModelCtrlData.bodyCtrlData = kinectManager.getBodyCtrlData(); 
        return cur_ModelCtrlData;
    }
}
