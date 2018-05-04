using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCtrl 
{
    private HandCtrl m_HandController;
    private BodyCtrl m_BodyController;

    public ModelCtrl(GameObject model)
    {
        
    }

    public void Init(GameObject model)
    {

    }

    //接收外部数据，移动模型
    public void MoveModel(ModelCtrlData modelCtrlData)
    {
        m_BodyController.MoveBody(modelCtrlData.bodyCtrlData);
        m_HandController.MoveHand(modelCtrlData.handCtrlData);
    }

}
