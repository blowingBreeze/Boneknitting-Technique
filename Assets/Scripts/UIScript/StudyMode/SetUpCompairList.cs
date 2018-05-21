using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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

    private void AddListItembByFileName(string astrFileName)
    {
        var tempHeadData = FileReader.GetHeadFromFile(astrFileName);
        var tempListItem = Instantiate(m_ListItemPrefab, m_Content.transform);
        tempListItem.GetComponentsInChildren<Image>()[1].overrideSprite= ToolFunction.CreateSpriteFromImage(DataPath.strDefaultPortraitFolder+"/"+tempHeadData.strPortrait);
        tempListItem.GetComponent<ClickCompairListItem>().SetFilePath(astrFileName);
    }
}
