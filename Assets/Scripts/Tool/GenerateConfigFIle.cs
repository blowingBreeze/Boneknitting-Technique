using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class GenerateConfigFIle : MonoBehaviour
{
    string ConfigFileName = "/Config.xml";
    string DefaultDirectory = "/Movies";
    void Start()
    {
        XmlDocument xml = new XmlDocument();
        xml.AppendChild(xml.CreateXmlDeclaration("1.0", "UTF-8", null));
        XmlElement root = xml.CreateElement("Root");
        XmlElement saveFolder= xml.CreateElement("SaveFloder");
        saveFolder.SetAttribute("DefaultFolder", DefaultDirectory);
        XmlElement CurrentFolder = xml.CreateElement("CurrentFolder");
        CurrentFolder.InnerText = DefaultDirectory;
        saveFolder.AppendChild(CurrentFolder);

        XmlElement HistoryFile = xml.CreateElement("HistoryFile");
        XmlElement history = xml.CreateElement("History");
        history.InnerText = "123";
        XmlElement h2 = xml.CreateElement("History");
        h2.InnerText = "456";
        HistoryFile.AppendChild(history);
        HistoryFile.AppendChild(h2);
        root.AppendChild(saveFolder);
        root.AppendChild(HistoryFile);
        xml.AppendChild(root);

        xml.Save(Application.dataPath + ConfigFileName);

        Debug.Log("GGGGGGGGGGGG");
    }
}
