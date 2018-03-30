using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipToWorkMode : MonoBehaviour
{
    public GameObject m_PlayModeCanvas;
    public GameObject m_RecordModeCanvas;
    public GameObject m_StudyModeCanvas;

    public void OnRecordMode()
    {
        Instantiate(m_RecordModeCanvas);
        Destroy(gameObject);
    }

    public void OnMovieMode()
    {
        Instantiate(m_PlayModeCanvas);
        Destroy(gameObject);
    }

    public void OnStudyMode()
    {
        Instantiate(m_StudyModeCanvas);
        Destroy(gameObject);
    }
}
