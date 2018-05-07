using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCtrl :MonoBehaviour
{
    public KinectManager kinectManager;
    private HandCtrl m_HandController;
    private BodyCtrl m_BodyController;

    private void Start()
    {
        m_HandController = GetComponentInChildren<HandCtrl>();
        m_BodyController = new BodyCtrl();
        if (kinectManager == null) kinectManager = KinectManager.Instance;
    }

    public void Init(GameObject model)
    {

    }

    //接收外部数据，移动模型
    public void MoveModel(ModelCtrlData modelCtrlData)
    {
        if (kinectManager.getBodyCtrl()) m_BodyController = kinectManager.getBodyCtrl();
        else
        {
            Debug.Log("KinectManager don't have a BodyCtrl !!");
            return;
        }
        if (m_BodyController.getBodyCtrlData() != null) m_BodyController.MoveBody(m_BodyController.getBodyCtrlData());

        m_HandController.MoveHand(modelCtrlData.handCtrlData);
    }

}
