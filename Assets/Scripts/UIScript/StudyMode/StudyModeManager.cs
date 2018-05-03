using System.Collections;
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
    private float fTimeCount;

    private GameObject m_HumenModelRef;
    private GameObject m_HumenModel;
    private StudyController m_StudyController;
    private GameObject m_ChartCanvas;
    private ChartController m_StudyModeChartController;

    // Use this for initialization
    void Awake()
    {
        m_strRefFileName = null;
        m_strFileName = null;
        bIsRecord = false;
        bIsStart = false;
        fTimeCount = 0.0f;

        m_HumenModelRef= Instantiate(m_HumenModelPrefab);
        m_HumenModel = Instantiate(m_HumenModelPrefab);
        m_ChartCanvas = Instantiate(m_ChartCanvasPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if(bIsStart)
        {
            m_StudyController.Update(fTimeCount, fTimeCount);
            fTimeCount += Time.deltaTime * 1000;
        }
    }

    public void SetRefFileName(string strFileName)
    {
        if(string.IsNullOrEmpty(strFileName))
        {
            return;
        }
        m_strRefFileName =strFileName;
    }

    public void SetCompairFileName(string strFileName)
    {
        if (string.IsNullOrEmpty(strFileName))
        {
            return;
        }
        m_strFileName = strFileName;
    }

    public void SetRecordAsTrue()
    {
        bIsRecord = true;
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
    }

    public void StartStudy()
    {
        m_StudyController.Ready();
        bIsStart = !bIsStart;
    }

    private void InitStudyModeChartCanvas()
    {
        m_StudyModeChartController = m_ChartCanvas.GetComponent<ChartController>();
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
