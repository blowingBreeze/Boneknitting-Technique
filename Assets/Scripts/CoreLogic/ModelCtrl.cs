using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCtrl 
{
    private HandCtrl m_HandController;
    private ModelCtrlData cur_ModelCtrlData;

    public ModelCtrl(GameObject model)
    {
        cur_ModelCtrlData = new ModelCtrlData();
    }

    public void Init(GameObject model)
    {

    }
    public ModelCtrlData getCurrentModelData()
    {
        return cur_ModelCtrlData;
    }

    //接收外部数据，移动模型
    public void MoveModel(ModelCtrlData modelCtrlData)
    {
        m_HandController.MoveHand(modelCtrlData.handCtrlData);
    }

}
