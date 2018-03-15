using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipToWork : MonoBehaviour
{
    public Canvas RecordModeCanvas;
    public Canvas MovieModeCanvas;
    public Canvas StudyModeCanvas;
    public void OnRecordMode()
    {
        Instantiate(RecordModeCanvas);
        Destroy(gameObject);
    }

    public void OnMovieMode()
    {
        Instantiate(MovieModeCanvas);
        Destroy(gameObject);
    }

    public void OnStudyMode()
    {
        Instantiate(StudyModeCanvas);
        gameObject.SetActive(false);
    }
}
