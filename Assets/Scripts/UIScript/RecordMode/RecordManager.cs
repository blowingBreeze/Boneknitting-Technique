using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager:MonoBehaviour
{
    public GameObject m_HumenModelPrefab;
    public GameObject m_ChartCanvasPrefab;

    private RecordController m_RecordController;
    private bool bIsStartRecord;
    private float fTimeCount;
    private FileWriter m_FileWriter;
    private GameObject m_HumenModel;
    private GameObject m_ChartCanvas;
    private ChartController m_RecordModeChartController;

    private int m_nFrameCount;
    private void Awake()
    {
        m_HumenModel = Instantiate(m_HumenModelPrefab);
        m_ChartCanvas = Instantiate(m_ChartCanvasPrefab);
        InitRecordModelChartController();

        m_RecordController = new RecordController(m_HumenModel);
        bIsStartRecord = false;
        fTimeCount = 0.0f;
        m_FileWriter = new FileWriter();
        m_nFrameCount = 0;
    }

    private void Update()
    {
        if (bIsStartRecord)
        {
            fTimeCount += Time.deltaTime * 1000;
            m_RecordController.Update();
            ModelCtrlData modelCtrlData = m_RecordController.GetCurrentData();
            m_FileWriter.CacheData(modelCtrlData);

            m_RecordModeChartController.UpdateLineChart(ChartType.CHART_SPEED, m_nFrameCount, TrailCurveDrawCtrl.Instance().lastSpeed(TrailType.EG_S1));
            m_RecordModeChartController.UpdateLineChart(ChartType.CHART_ACCELERATE, m_nFrameCount, TrailCurveDrawCtrl.Instance().lastAcceleration(TrailType.EG_S1));
            m_RecordModeChartController.UpdateLineChart(ChartType.CHART_CURVATURE, m_nFrameCount, TrailCurveDrawCtrl.Instance().lastCurvature(TrailType.EG_S1));
            m_RecordModeChartController.UpdateLineChart(ChartType.CHART_TORSION, m_nFrameCount, TrailCurveDrawCtrl.Instance().lastTorsion(TrailType.EG_S1));
            ++m_nFrameCount;
        }
    }

    public RecordController GetRecordController()
    {
        return m_RecordController;
    }

    public bool InitDevice()
    {
        return m_RecordController.InitDevice();
    }

    public void DisconnnectDevice()
    {
        m_RecordController.DisconnectDevice();
    }

    public bool IsStartRecord()
    {
        return bIsStartRecord;
    }

    public void StartOrStopRecord()
    {
        bIsStartRecord = !bIsStartRecord;
    }

    public void SaveDataToFile(MovieHeadData headData, string strFileName, float fStartTime = -1, float fEndTime = -1)
    {
        m_FileWriter.SaveDataToFile(headData, strFileName, fStartTime, fEndTime);
    }

    public ModelCtrlData GetModelCtrlDataByTime(float fTime)
    {
        return m_FileWriter.GetModelCtrlDataByTime(fTime);
    }

    /// <summary>
    /// 返回一个以毫秒为单位的录制时间长度
    /// </summary>
    /// <returns></returns>
    public float GetTimeCount()
    {
        return fTimeCount;
    }

    private void InitRecordModelChartController()
    {
        m_RecordModeChartController = m_ChartCanvas.GetComponent<ChartController>();
        m_RecordModeChartController.InitChart();
        m_RecordModeChartController.HideRefLineChart(ChartType.CHART_SPEED);
        m_RecordModeChartController.HideRefLineChart(ChartType.CHART_ACCELERATE);
        m_RecordModeChartController.HideRefLineChart(ChartType.CHART_CURVATURE);
        m_RecordModeChartController.HideRefLineChart(ChartType.CHART_TORSION);
    }

    public GameObject GetRecordModeChartCanvas()
    {
        return m_ChartCanvas;
    }

    private void OnDestroy()
    {
        Destroy(m_HumenModel);
        Destroy(m_ChartCanvas);
    }
}
