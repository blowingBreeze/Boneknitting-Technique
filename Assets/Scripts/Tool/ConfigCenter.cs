using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


public class XmlString
{
    public const string strConfigFilePath = "/Config.xml";

    public const  string strDefaultSaveFolder = "/Movies";
    public const string strSaveDirectoryParentNode = "SaveFloder";
    public const string strHistoryParentNode = "HistoryFile";
    public const   string strSaveDirectoryNodeName = "CurrentFolder";
    public const  string strHistoryFilePathNodeName = "History";
}

public class ConfigCenter
{
    private static ConfigCenter ConfigCenterInstance;
    private ConfigCenter()
    {
        strSaveDirectory = null;
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
    XmlDocument xml;

    private string strSaveDirectory;
    private List<string> HistoryFilePathList;
    private float m_fFPS;

    public string GetDefaultDirPath()
    {
        return strSaveDirectory;
    }
    public List<string> GetHistoryFilePathList()
    {
        return HistoryFilePathList;
    }

    public float GetFPS()
    {
        return m_fFPS;
    }

    /// <summary>
    /// 读取Config.xml文件，初始化ConfigCenter,路径；
    /// </summary>
    /// <param name="path">配置文件相对于Application.datapath路径</param>
    /// <returns></returns>
    public bool ConfigDataInit(string path)
    {
        xml = new XmlDocument();
        string content = System.IO.File.ReadAllText(Application.dataPath+path);
        xml.LoadXml(content);

        XmlElement players = xml.DocumentElement;//获取根元素  
        foreach (XmlNode player in players.ChildNodes)//遍历所有子节点  
        {
            foreach (XmlNode node in player.ChildNodes)
            {
                XmlElement xe = (XmlElement)node;
                switch (xe.Name)
                {
                    case XmlString.strSaveDirectoryNodeName:
                        Debug.Log(xe.InnerText);
                        strSaveDirectory = xe.InnerText;
                        break;
                    case XmlString.strHistoryFilePathNodeName:
                        Debug.Log(xe.InnerText);
                        HistoryFilePathList.Add(xe.InnerText);
                        break;
                }
            }
        }

        return true;
    }

    public void SetSaveFilePath(string path)
    {
        if(string.IsNullOrEmpty(path))
        {
            return;
        }
        var SaveFileDirectoryNode = xml.GetElementsByTagName(XmlString.strSaveDirectoryNodeName);
        SaveFileDirectoryNode[0].InnerText = path;
    }

    public void DeleteHistoryFile()
    {
        var HistoryNode= xml.GetElementsByTagName(XmlString.strHistoryFilePathNodeName);
        foreach( XmlNode node in HistoryNode)
        {
            xml.RemoveChild(node);
        }
    }

    public void AddHistoryFile(string path)
    {
        if(string.IsNullOrEmpty(path))
        {
            return;
        }
        var HistoryFile = xml.GetElementsByTagName(XmlString.strHistoryParentNode);
        XmlElement history = xml.CreateElement("History");
        history.InnerText = path;
        HistoryFile[0].AppendChild(history);
        xml.Save(Application.dataPath+XmlString.strConfigFilePath);
    }
}
