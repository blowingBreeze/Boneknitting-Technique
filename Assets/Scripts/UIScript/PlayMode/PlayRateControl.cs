using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayRateControl : MonoBehaviour
{
    public Slider m_TimeSlider;
    public Text m_TimeCount;
    public Text m_AccelerateCount;


    private MoviePlayManager m_MoviePlayManager;
    private string m_strTotalTime;

    private void Start()
    {
        m_MoviePlayManager = GetComponent<MoviePlayManager>();
        m_strTotalTime = ToolFunction.TranslateToMMSS(m_MoviePlayManager.GetTotalTime());
    }

    public void LateUpdate()
    {
        if (m_MoviePlayManager.IsStart())
        {
            m_TimeCount.text = ToolFunction.TranslateToMMSS( m_MoviePlayManager.GetCurrentTime()) + "/" + m_strTotalTime;
            m_TimeSlider.value = m_MoviePlayManager.GetCurrentTime() / m_MoviePlayManager.GetTotalTime();
        }
    }

    public void OnTimeChanged()
    {
        m_MoviePlayManager.SetCurrentTime(m_TimeSlider.value);
        Debug.Log("ValueChanged"+m_TimeSlider.value);
    }

    public void OnAccelerateClick()
    {
        m_MoviePlayManager.Accelerate();
        m_AccelerateCount.text = "x" + m_MoviePlayManager.GetAccelerate();
    }

    public void OnStartOrStopClick()
    {
        m_MoviePlayManager.StartOrStop();
    }

    public void OnDeaccelerateClick()
    {
        m_MoviePlayManager.Deaccelerate();
        m_AccelerateCount.text = "x" + m_MoviePlayManager.GetAccelerate();
    }
}
