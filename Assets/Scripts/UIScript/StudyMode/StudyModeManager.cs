﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyModeManager : MonoBehaviour
{
    public GameObject m_HumenModelPrefab;
    public GameObject m_ChartCanvasPrefab;

    private string m_strRefFileName; //参照数据文件路径
    private string m_strFileName; //需对比的文件路径
    private bool bIsRecord;
    private bool bIsStart;

    private GameObject m_HumenModelRef;
    private GameObject m_HumenModel;
    private StudyController m_StudyController;
    private GameObject m_ChartCanvas;
    private ChartController m_StudyModeChartController;

    private VideoRateCtrl m_RefRateController;
    private VideoRateCtrl m_RateController;
    private float fRefTimeClock;
    private float fTimeClock;
    // Use this for initialization
    void Awake()
    {
        m_strRefFileName = null;
        m_strFileName = null;
        bIsRecord = false;
        bIsStart = false;

        m_HumenModelRef= Instantiate(m_HumenModelPrefab);
        m_HumenModel = Instantiate(m_HumenModelPrefab);
        m_ChartCanvas = Instantiate(m_ChartCanvasPrefab);

        fRefTimeClock = 0f;
        fTimeClock = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(bIsStart)
        {
            fRefTimeClock += Time.deltaTime * 1000;
            fTimeClock += Time.deltaTime * 1000;
            if(fRefTimeClock>=m_RefRateController.fIntervalTime)
            {
                m_RefRateController.nCurrentFrame += 1;
            }
            if(fTimeClock>=m_RateController.fIntervalTime)
            {
                m_RateController.nCurrentFrame += 1;
            }

            m_StudyController.Update(m_RefRateController.nCurrentFrame, m_RateController.nCurrentFrame);
            m_StudyModeChartController.UpdateRefLineChart(ChartType.CHART_SPEED, m_RefRateController.nCurrentFrame, TrailCurveDrawCtrl.Instance().lastSpeed(TrailType.EG_S1));
            m_StudyModeChartController.UpdateRefLineChart(ChartType.CHART_ACCELERATE, m_RefRateController.nCurrentFrame, TrailCurveDrawCtrl.Instance().lastAcceleration(TrailType.EG_S1));
            m_StudyModeChartController.UpdateRefLineChart(ChartType.CHART_CURVATURE, m_RefRateController.nCurrentFrame, TrailCurveDrawCtrl.Instance().lastCurvature(TrailType.EG_S1));
            m_StudyModeChartController.UpdateRefLineChart(ChartType.CHART_TORSION, m_RefRateController.nCurrentFrame, TrailCurveDrawCtrl.Instance().lastTorsion(TrailType.EG_S1));
            m_StudyModeChartController.UpdateLineChart(ChartType.CHART_SPEED, m_RateController.nCurrentFrame, TrailCurveDrawCtrl.Instance().lastSpeed(TrailType.EG_S1));
            m_StudyModeChartController.UpdateLineChart(ChartType.CHART_ACCELERATE, m_RateController.nCurrentFrame, TrailCurveDrawCtrl.Instance().lastAcceleration(TrailType.EG_S1));
            m_StudyModeChartController.UpdateLineChart(ChartType.CHART_CURVATURE, m_RateController.nCurrentFrame, TrailCurveDrawCtrl.Instance().lastCurvature(TrailType.EG_S1));
            m_StudyModeChartController.UpdateLineChart(ChartType.CHART_TORSION, m_RateController.nCurrentFrame, TrailCurveDrawCtrl.Instance().lastTorsion(TrailType.EG_S1));

        }
    }

    public void SetRefFileName(string strFileName)
    {
        if(string.IsNullOrEmpty(strFileName))
        {
            return;
        }
        m_strRefFileName =strFileName;
        var tempHeadData = FileReader.GetHeadFromFile(m_strRefFileName);
        m_RefRateController = new VideoRateCtrl(tempHeadData.nTotalFrameCount, 1000 / tempHeadData.nFPS);
    }

    public void SetCompairFileName(string strFileName)
    {
        if (string.IsNullOrEmpty(strFileName))
        {
            return;
        }
        m_strFileName = strFileName;
        var tempHeadData = FileReader.GetHeadFromFile(m_strRefFileName);
        m_RateController = new VideoRateCtrl(tempHeadData.nTotalFrameCount, 1000 / tempHeadData.nFPS);
    }

    public void SetRecordAsTrue()
    {
        bIsRecord = true;
        m_RateController = new VideoRateCtrl(0, 1000 / 30);
    }

    public void Prepare()
    {
        if (bIsRecord)
        {
            m_StudyController = new StudyControllerFileRecord(m_HumenModelRef, m_HumenModel, m_strRefFileName);
        }
        else
        {
            m_StudyController = new StudyControllerFileFile(m_HumenModelRef, m_HumenModel, m_strRefFileName, m_strFileName);
        }
        m_StudyController.Ready();
    }

    public void StartStudy()
    {
        bIsStart = !bIsStart;
    }

    private void InitStudyModeChartCanvas()
    {
        m_StudyModeChartController = m_ChartCanvas.GetComponent<ChartController>();
        m_StudyModeChartController.InitChart();
        m_StudyModeChartController.InitRefChart();
    }

    public GameObject GetStudyModeChartCanvas()
    {
        return m_ChartCanvas;
    }

    private void OnDestroy()
    {
        Destroy(m_HumenModelRef);
        Destroy(m_HumenModel);
        Destroy(m_ChartCanvas);
    }
}
