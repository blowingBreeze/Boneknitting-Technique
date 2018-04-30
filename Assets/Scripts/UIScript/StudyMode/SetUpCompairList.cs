using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SetUpCompairList : MonoBehaviour
{
    public RectTransform m_Content;
    public GameObject m_ListItemPrefab;

    private void Start()
    {
        ListMovieByDirectory(ConfigCenter.GetConfigCenterInstance().GetDefaultDirPath());
        ListMovieByFileNames(ConfigCenter.GetConfigCenterInstance().GetHistoryFilePathList());
    }

    private void ListMovieByDirectory(string strDirectoryPath)
    {
        if (string.IsNullOrEmpty(strDirectoryPath))
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
                SetCompareListItemByFileName(FileList[tFileIndex].FullName);
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
            SetCompareListItemByFileName(FileNameList[tIndex]);
        }
    }

    private void SetCompareListItemByFileName(string astrFileName)
    {
        var tempHeadData = FileReader.GetHeadFromFile(astrFileName);
        var tempListItem = Instantiate(m_ListItemPrefab, m_Content.transform);
        ///依据头部信息载入图片,设置按钮信息 TODO
        //tempListItem.GetComponent<Image>().overrideSprite;
        tempListItem.GetComponent<ClickCompairListItem>().SetFilePath(astrFileName);
    }
}
