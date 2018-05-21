﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MovieCatalogueScript : MonoBehaviour
{
    public RectTransform m_Content;
    public GameObject m_MovieListItemPrefab;

    private void Start()
    {
        ListMovieByDirectory(ConfigCenter.Instance().GetDefaultDirPath());
        ListMovieByFileNames(ConfigCenter.Instance().GetHistoryFilePathList());
    }

    private void ListMovieByDirectory(string strDirectoryPath)
    {
        if (string.IsNullOrEmpty(strDirectoryPath))
        {
            return;
        }
        var dir = new DirectoryInfo(strDirectoryPath);
        if (dir.Exists)
        {
            var FileList = dir.GetFiles();
            for (int tFileIndex = 0; tFileIndex < FileList.Length; ++tFileIndex)
            {
                if (ToolFunction.IsExtension(FileList[tFileIndex].FullName, ".txt"))
                {
                    AddListItembByFileName(FileList[tFileIndex].FullName);
                }
            }
        }
    }

    private void ListMovieByFileNames(List<string> FileNameList)
    {
        if (FileNameList == null)
        {
            return;
        }

        for (int tIndex = 0; tIndex < FileNameList.Count; ++tIndex)
        {
            AddListItembByFileName(FileNameList[tIndex]);
        }
    }

    public void AddListItembByFileName(string astrFileName)
    {
        if (string.IsNullOrEmpty(astrFileName))
        {
            return;
        }
        var tempHeadData = FileReader.GetHeadFromFile(astrFileName);
        var tempListItem = Instantiate(m_MovieListItemPrefab, m_Content.transform);
        Debug.Log(ToolFunction.GetDefaultPortraitPathByName(tempHeadData.strPortrait, ".jpg"));
        tempListItem.GetComponent<Image>().overrideSprite = ToolFunction.CreateSpriteFromImage(ToolFunction.GetDefaultPortraitPathByName( tempHeadData.strPortrait,".jpg"));
        tempListItem.GetComponentInChildren<Text>().text = tempHeadData.strDoctorName + "\n" + tempHeadData.strGenerateTime;
        tempListItem.GetComponent<ClickMovieListItem>().SetFilePath(astrFileName);
    }
}
