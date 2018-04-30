using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCtrl 
{
    HandCtrl m_HandController;

    public ModelCtrl(GameObject model)
    {
        
    }

    public void Init(GameObject model)
    {

    }

    //接收外部数据，移动模型
    public void MoveModel(ModelCtrlData modelCtrlData)
    {
        m_HandController.MoveHand(ModelCtrlData.m_HandData);
    }

}
