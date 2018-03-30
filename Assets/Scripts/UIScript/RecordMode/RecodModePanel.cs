using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecodModePanel : MonoBehaviour
{
    public GameObject m_BtnCalibrate;
    public GameObject m_BtnBeginRecord;
    public GameObject m_BtnStopRecord;
    public GameObject m_BtnSave;
    public GameObject m_StartCanvas;

    public void BtnBeginRecord()
    {
        m_BtnStopRecord.SetActive(true);
        Destroy(m_BtnBeginRecord);
    }

    public void BtnCalibrate()
    {
        m_BtnBeginRecord.SetActive(true);
        Destroy(m_BtnCalibrate);
    }

    public void BtnStopRecord()
    {
        m_BtnSave.SetActive(true);
        Destroy(m_BtnStopRecord);
    }

    public void BtnReturn()
    {
        Instantiate(m_StartCanvas);
        Destroy(gameObject);
    }
}
