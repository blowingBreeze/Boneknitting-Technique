using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecodModePanel : MonoBehaviour
{
    public GameObject m_BtnCalibrate;
    public GameObject m_BtnBeginRecord;
    public GameObject m_BtnStopRecord;
    public GameObject m_SaveControlPanel;
    public GameObject m_StartCanvas;

    public Text m_TimeCount;

    private RecordManager m_RecordManager;

    private void Start()
    {
        m_RecordManager = GetComponent<RecordManager>();
    }

    private void Update()
    {
        if(m_RecordManager.IsStartRecord())
        {
            m_TimeCount.text = ToolFunction.TranslateToMSM(m_RecordManager.GetTimeCount());
        }
    }

    public void BtnBeginRecord()
    {
        m_BtnStopRecord.SetActive(true);
        m_RecordManager.StartOrStopRecord();
        Destroy(m_BtnBeginRecord);
    }

    public void BtnCalibrate()
    {
        m_BtnBeginRecord.SetActive(true);
        m_RecordManager.InitDevice();
        Destroy(m_BtnCalibrate);
    }

    public void BtnStopRecord()
    {
        m_SaveControlPanel.SetActive(true);
        m_RecordManager.StartOrStopRecord();
        m_RecordManager.DisconnnectDevice();
        Destroy(m_BtnStopRecord);
    }

    public void BtnReturn()
    {
        Instantiate(m_StartCanvas);
        m_RecordManager.DisconnnectDevice();
        Destroy(gameObject);
    }
}
