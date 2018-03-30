using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickMovieListItem : MonoBehaviour
{
    public GameObject PlayModeCanvas;

    private GameObject StartModeCanvas;
    private string strFilePath;

    private void Start()
    {
        StartModeCanvas = GameObject.FindGameObjectWithTag("StartCanvas");
    }
    public void OnClickMovieListItem()
    {
        Instantiate(PlayModeCanvas);
        Destroy(StartModeCanvas);
    }

    public void SetFilePath(string astrFilePath)
    {
        strFilePath = astrFilePath;
    }
}
