using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoviePlayManager : MonoBehaviour
{
    public GameObject m_HumenModelPrefab;
    public GameObject m_ChartCanvasPrefab;

    private GameObject m_HumenModel;
    private GameObject m_ChartCanvas;

    private string m_strFileName;
    private PlayController m_PlayController;
    private FileReader m_FileReader;
    private VideoRateCtrl m_VIdeoRateController;
    private ChartController m_PlayModeChartController;
    private bool bIsPlay;
    private int m_nFrameCount;

    private void Awake()
    {
        m_HumenModel = Instantiate(m_HumenModelPrefab);
        m_ChartCanvas = Instantiate(m_ChartCanvasPrefab);
        InitPlayModeChart();

        m_strFileName = null;
        m_PlayController = new PlayController(m_HumenModel);
        m_FileReader = null;
        m_VIdeoRateController = null;

        bIsPlay = false;
        m_nFrameCount = 0;
    }

    private void Update()
    {
        if (bIsPlay)
        {
            m_VIdeoRateController.fCurrentTime += Time.deltaTime*1000;// m_VIdeoRateController.fIntervalTime;
            var modelCtrlData = m_FileReader.PraseDataByTime(m_VIdeoRateController.fCurrentTime);

            m_PlayController.Update(modelCtrlData);

            m_PlayModeChartController.UpdateLineChart(ChartType.CHART_SPEED, m_nFrameCount, TrailCurveDrawCtrl.Instance().lastSpeed(TrailType.EG_S1));
            m_PlayModeChartController.UpdateLineChart(ChartType.CHART_ACCELERATE, m_nFrameCount, TrailCurveDrawCtrl.Instance().lastAcceleration(TrailType.EG_S1));
            m_PlayModeChartController.UpdateLineChart(ChartType.CHART_CURVATURE, m_nFrameCount, TrailCurveDrawCtrl.Instance().lastCurvature(TrailType.EG_S1));
            m_PlayModeChartController.UpdateLineChart(ChartType.CHART_TORSION, m_nFrameCount, TrailCurveDrawCtrl.Instance().lastTorsion(TrailType.EG_S1));
            ++m_nFrameCount;
        }
    }

    public void SetFileName(string strFileName)
    {
        m_strFileName = strFileName;
        m_FileReader = new FileReader(strFileName);
        var tempDataHead = FileReader.GetHeadFromFile(strFileName);
        m_VIdeoRateController = new VideoRateCtrl(tempDataHead.fTotalTime, tempDataHead.fIntervalTime, tempDataHead.fCurrentTime);
    }

    public void StartOrStop()
    {
        bIsPlay = !bIsPlay;
    }

    public bool IsStart()
    {
        return bIsPlay;
    }

    public void Accelerate()
    {
        m_VIdeoRateController.fIntervalTime *= 2;
    }

    public void Deaccelerate()
    {
        m_VIdeoRateController.fIntervalTime *= 0.5f;
    }

    public float GetAccelerate()
    {
        return m_VIdeoRateController.GetAccelerate();
    }

    public void SetCurrentTime(float fRate)
    {
        m_VIdeoRateController.fCurrentTime = m_VIdeoRateController.fTotalTime * fRate;
    }

    public float GetCurrentTime()
    {
        return m_VIdeoRateController.fCurrentTime;
    }

    public float GetTotalTime()
    {
        return m_VIdeoRateController.fTotalTime;
    }

    public PlayController GetPlayController()
    {
        return m_PlayController;
    }

    private void InitPlayModeChart()
    {
        m_PlayModeChartController = m_ChartCanvas.GetComponent<ChartController>();
        m_PlayModeChartController.InitChart();
        m_PlayModeChartController.HideRefLineChart(ChartType.CHART_SPEED);
        m_PlayModeChartController.HideRefLineChart(ChartType.CHART_ACCELERATE);
        m_PlayModeChartController.HideRefLineChart(ChartType.CHART_CURVATURE);
        m_PlayModeChartController.HideRefLineChart(ChartType.CHART_TORSION);
    }

    public GameObject GetChartCanvas()
    {
        return m_ChartCanvas;
    }

    private void OnDestroy()
    {
        Destroy(m_HumenModel);
        Destroy(m_ChartCanvas);
    }
}
