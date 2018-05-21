using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

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
        GameObject tPlayModeCanvas= Instantiate(PlayModeCanvas);
        tPlayModeCanvas.GetComponent<MoviePlayManager>().SetFileName(strFilePath);
        Destroy(StartModeCanvas);
    }

    public void OnClickDelet_Btn()
    {
        var filepath = GetComponent<ClickMovieListItem>().GetFilePath();
        ConfigCenter.Instance().DeleteFileByPath(GetComponent<ClickMovieListItem>().GetFilePath());
        var temphead = FileReader.GetHeadFromFile(filepath);
        var portraitpath = ToolFunction.GetDefaultPortraitPathByName(temphead.strPortrait, ".jpg");
        if (File.Exists(portraitpath))
        {
            File.Delete(portraitpath);
        }
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }
        Destroy(gameObject);
    }

    public void SetFilePath(string astrFilePath)
    {
        strFilePath = astrFilePath;
    }

    public string GetFilePath()
    {
        return strFilePath;
    }
}
