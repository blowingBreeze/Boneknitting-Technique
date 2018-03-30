using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigCenter
{
    private static ConfigCenter ConfigCenterInstance;
    private ConfigCenter()
    {
        strDefaultDirectoryPath = null;
        HistoryFilePathList = new List<string>();
    }
    public static ConfigCenter GetConfigCenterInstance()
    {
        if (ConfigCenterInstance == null)
        {
            ConfigCenterInstance = new ConfigCenter();
        }
        return ConfigCenterInstance;
    }

    private string strDefaultDirectoryPath;
    private List<string> HistoryFilePathList;


    public void ConfigDataInit()
    {

    }

    public string GetDefaultDirPath()
    {
        return strDefaultDirectoryPath;
    }
    public List<string> GetHistoryFilePathList()
    {
        return HistoryFilePathList;
    }

}
