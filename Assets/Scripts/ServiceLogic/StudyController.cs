using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class StudyController
{
    // Update is called once per frame
    abstract public bool Ready();
    abstract public void Update(ModelCtrlData modelDataRef, ModelCtrlData modelData);
    abstract public float GetAppraiseResult();
}

internal class StudyControllerFileFile : StudyController
{
    private PlayController m_PlayControllerRef;
    private PlayController m_PlayController;
    private TrailCurveAppraiseCtrl m_TrailCurveAppraise;

    public StudyControllerFileFile(GameObject modelRef, GameObject model)
    {
        Init(modelRef, model);
    }

    bool Init(GameObject modelRef, GameObject model)
    {
        m_PlayControllerRef = new PlayController(modelRef);
        m_PlayController = new PlayController(model);
        m_TrailCurveAppraise = new TrailCurveAppraiseCtrl();
        return true;
    }

    public override void Update(ModelCtrlData modelDataRef, ModelCtrlData modelData)
    {
        m_PlayControllerRef.Update(modelDataRef);
        m_PlayController.Update(modelData);
        m_TrailCurveAppraise.RecvCompairTrailData(modelDataRef, modelData);
    }

    public override float  GetAppraiseResult() 
    {
        return m_TrailCurveAppraise.AppraiseTrailCurve();
    }

    public override bool Ready()
    {
        ;
    }
}

internal class StudyControllerFileRecord :StudyController
{
    private PlayController m_PlayControllerRef;
    private RecordController m_RecordController;
    private TrailCurveAppraiseCtrl m_TrailCurveAppraise;

    public StudyControllerFileRecord(GameObject modelRef, GameObject model)
    {
        Init(modelRef, model);
    }

    bool Init(GameObject modelRef, GameObject model)
    {
        m_PlayControllerRef = new PlayController(modelRef);
        m_RecordController = new RecordController(model);
        m_TrailCurveAppraise = new TrailCurveAppraiseCtrl();
        return true;
    }

    public override void Update(ModelCtrlData modelDataRef,ModelCtrlData)
    {
        m_PlayControllerRef.Update(modelDataRef);
        m_RecordController.Update();
        ModelCtrlData modelData = m_RecordController.GetCurrentFrameData();
        m_TrailCurveAppraise.RecvCompairTrailData(modelDataRef, modelData);
    }

    public override float GetAppraiseResult()
    {
        return m_TrailCurveAppraise.AppraiseTrailCurve();
    }

    public override bool Ready()
    {
        m_RecordController.InitDevice();
    }
}