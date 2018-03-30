using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController
{
    private string strFileName;
    private VideoRateCtrl m_VideoRateController;
    //private ModelCtrl m_ModelController;
    //private DataCtrl m_DataController ,即输入源控制
    private TrailCurveCtrl m_TrailController;

    public PlayController()
    {
        Init();
    }

    private bool Init()
    {
        m_VideoRateController = new VideoRateCtrl();
        m_TrailController = new TrailCurveCtrl();
        return true;
    }
    
    public void SetFileName(string astrFileName)
    {
        strFileName = astrFileName;
    }

    public void Update()
    {

    }

    public void SetAccelerate(float fRatio)
    {

    }

    public void SetCurrentTime(float fRatio)
    {

    }

    public void SwitchTrail(TrailType trailType,bool IsOn)
    {
        m_TrailController.SwitchTrailCurve(trailType, IsOn);
    }
}
