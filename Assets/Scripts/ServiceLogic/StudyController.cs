using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class StudyController
{
    // Update is called once per frame
    abstract public bool Ready();
    abstract public void Update(float fTimeRef, float fTime);
    abstract public float GetAppraiseResult();
}

internal class StudyControllerFileFile : StudyController
{
    private PlayController m_PlayControllerRef;
    private PlayController m_PlayController;
    private FileReader m_ReaderRef;
    private FileReader m_Reader;
    private TrailCurveAppraiseCtrl m_TrailCurveAppraise;


    public StudyControllerFileFile(GameObject modelRef, GameObject model,string strFileNameRef,string strFileName)
    {
        Init(modelRef, model,strFileNameRef,strFileName);
    }

    bool Init(GameObject modelRef, GameObject model, string strFileNameRef, string strFileName)
    {
        m_PlayControllerRef = new PlayController(modelRef);
        m_PlayController = new PlayController(model);
        m_ReaderRef = new FileReader(strFileNameRef);
        m_Reader = new FileReader(strFileName);
        m_TrailCurveAppraise = new TrailCurveAppraiseCtrl();
        return true;
    }

    public override void Update(float fTimeRef, float fTime)
    {
        var modelDataRef = m_ReaderRef.PraseDataByTime(fTimeRef);
        var modelData = m_Reader.PraseDataByTime(fTime);
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
        return true;
    }
}

internal class StudyControllerFileRecord :StudyController
{
    private PlayController m_PlayControllerRef;
    private RecordController m_RecordController;
    private TrailCurveAppraiseCtrl m_TrailCurveAppraise;
    private FileReader m_ReaderRef;
    public StudyControllerFileRecord(GameObject modelRef, GameObject model, string strFileNameRef)
    {
        Init(modelRef, model,strFileNameRef);
    }

    bool Init(GameObject modelRef, GameObject model, string strFileNameRef)
    {
        m_PlayControllerRef = new PlayController(modelRef);
        m_RecordController = new RecordController(model);
        m_TrailCurveAppraise = new TrailCurveAppraiseCtrl();
        m_ReaderRef = new FileReader(strFileNameRef);
        return true;
    }

    public override void Update(float fTimeRef, float fTime)
    {
        var modelDataRef = m_ReaderRef.PraseDataByTime(fTimeRef);
        var modelData = m_RecordController.GetCurrentData();
        m_PlayControllerRef.Update(modelDataRef);
        m_RecordController.Update();
        m_TrailCurveAppraise.RecvCompairTrailData(modelDataRef, modelData);
    }

    public override float GetAppraiseResult()
    {
        return m_TrailCurveAppraise.AppraiseTrailCurve();
    }

    public override bool Ready()
    {
        m_RecordController.InitDevice();
        return true;
    }
}