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
        m_fFPS = 60;
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

    private float m_fFPS;

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

    public float GetFPS()
    {
        return m_fFPS;
    }

}
