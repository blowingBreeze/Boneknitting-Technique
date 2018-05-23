using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class DeviceCtrl
{

    private ModelCtrlData cur_ModelCtrlData = new ModelCtrlData();
    private BodyCtrlData test_data = new BodyCtrlData();
    // private instance of the KinectManager
    private KinectManager kinectManager;
    private DTGloveManager gloveManager;
    private LpSensorManager lpSensorManager;
    private Thread lpThread;

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

        if (lpSensorManager == null)
        {
            lpSensorManager = new LpSensorManager(ref test_data, ref cur_ModelCtrlData.wristCtrlData, 23333, "00:04:3E:9E:00:C5", 20);

            if (lpSensorManager.Init())
            {
                lpThread = new Thread(new ThreadStart(lpSensorManager.receiveData));
                lpThread.Start();
            }
        }


        return true;

    }

    //断开设备连接
    public void DisconnectDevice()
    {
        //gloveManager.DisconnectDevice();
        //lpSensorManager.DisconnectDevice();
        lpThread.Abort();
    }

    /// <summary>
    /// 从各个设备读取当前帧数据，整合成为当前帧可以用于控制模型的数据
    /// </summary>
    /// <returns></returns>
    public ModelCtrlData AcquireData()
    {
        //Quaternion q = new Quaternion(cur_ModelCtrlData.bodyCtrlData.jointRotation[12].x, cur_ModelCtrlData.bodyCtrlData.jointRotation[12].y, cur_ModelCtrlData.bodyCtrlData.jointRotation[12].z, cur_ModelCtrlData.bodyCtrlData.jointRotation[12].w);
        //kinectManager.setJointRotation(q,12);
        cur_ModelCtrlData.bodyCtrlData = kinectManager.getBodyCtrlData();
        gloveManager.AcquireHandData(ref cur_ModelCtrlData.handCtrlData);
        Debug.Log(test_data.jointRotation[12].x);
        GameObject.FindGameObjectWithTag("RightHand").transform.rotation = test_data.jointRotation[12];
        cur_ModelCtrlData.bodyCtrlData.HandRightPos = GameObject.FindGameObjectWithTag("RightHand").transform.position;
        cur_ModelCtrlData.bodyCtrlData.HandLeftPos = GameObject.FindGameObjectWithTag("LeftHand").transform.position;

        Debug.Log(cur_ModelCtrlData.bodyCtrlData.HandRightPos);
        return cur_ModelCtrlData;
    }
}
