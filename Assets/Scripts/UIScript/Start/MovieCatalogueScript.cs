using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MovieCatalogueScript : MonoBehaviour
{
    public RectTransform m_Content;
    public GameObject m_MovieListItemPrefab;

    private FileReader m_FileReader;

    private void Start()
    {
        m_FileReader = new FileReader();
        ListMovieByDirectory(ConfigCenter.GetConfigCenterInstance().GetDefaultDirPath());
        ListMovieByFileNames(ConfigCenter.GetConfigCenterInstance().GetHistoryFilePathList());
    }

    private void ListMovieByDirectory(string strDirectoryPath)
    {
        if(string.IsNullOrEmpty( strDirectoryPath))
        {
            Debug.Log("null directory path");
            return;
        }
        var dir = new DirectoryInfo(strDirectoryPath);
        if (dir.Exists)
        {
            var FileList = dir.GetFiles();
            for (int tFileIndex = 0; tFileIndex < FileList.Length; ++tFileIndex)
            {
                SetListItemByFileName(FileList[tFileIndex].FullName);
            }
        }
    }

    private void ListMovieByFileNames(List<string> FileNameList)
    {
        for(int tIndex=0;tIndex<FileNameList.Count;++tIndex)
        {
            SetListItemByFileName(FileNameList[tIndex]);
        }
    }

    private void SetListItemByFileName(string astrFileName)
    {
        var tempHeadData = FileReader.GetHeadFromFile(astrFileName);
        var tempListItem = Instantiate(m_MovieListItemPrefab, m_Content.transform);
        ///依据头部信息载入图片,设置按钮信息 TODO
        //tempListItem.GetComponent<Image>().overrideSprite;
        tempListItem.GetComponent<ClickMovieListItem>().SetFilePath(astrFileName);
    }
}
