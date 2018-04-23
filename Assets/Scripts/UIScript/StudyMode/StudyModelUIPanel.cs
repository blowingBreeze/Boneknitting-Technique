using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudyModelUIPanel : MonoBehaviour
{
    public GameObject m_StartCanvasPrefab;
    public GameObject m_ComPairMovieList;
    public GameObject m_StudyUIPanel;
    public GameObject m_StudyPanel;
    public Button m_BtnRefData;
    public GameObject m_CompairData;
    public Text m_WarnText;


    private StudyModeManager m_studyManager;
    private bool bIsRef;
    private bool bIsRefFileSet;
    private bool bIsCompairFileSet;
    private bool bIsRecordSet;
    // Use this for initialization
    void Start()
    {
        m_studyManager = GetComponent<StudyModeManager>();
        bIsRef = true;
        bIsRefFileSet = false;
        bIsCompairFileSet = false;
        bIsRecordSet = false;
    }

    public void BtnRefData()
    {
        bIsRef = true;
        m_ComPairMovieList.SetActive(true);
    }

    public void BtnCurrentData()
    {
        bIsRef = false;
        m_ComPairMovieList.SetActive(true);
    }

    public void BtnRecord()
    {
        m_studyManager.SetRecordAsTrue();
        bIsRecordSet = true;
        m_CompairData.SetActive(true);
        m_CompairData.GetComponentInChildren<Text>().text = "将录制";
    }

    public void BtnPrepare()
    {
        bool bIsOK = true;
        if (!bIsRefFileSet)
        {
            m_WarnText.text = "请选择参考数据";
            bIsOK = false;
        }
        else if (!(bIsCompairFileSet || bIsRecordSet))
        {
            m_WarnText.text = "请选择现有数据或实时录制";
            bIsOK = false;
        }

        if (bIsOK)
        {
            m_StudyUIPanel.SetActive(false);
            m_StudyPanel.SetActive(true);
            m_studyManager.Prepare();
        }
    }

    public void BtnStartStudy()
    {
        m_studyManager.StartStudy();
    }

    public void BtnReturn()
    {
        Instantiate(m_StartCanvasPrefab);
        Destroy(gameObject);
    }

    public void SetFileName(string strFileName)
    {
        if (bIsRef)
        {
            m_studyManager.SetRefFileName(strFileName);
            bIsRefFileSet = true;
            var tempHeadData = FileReader.GetHeadFromFile(strFileName);
            m_BtnRefData.image.overrideSprite = ToolFunction.CreateSpriteFromImage(tempHeadData.strPortraitPath);
            m_BtnRefData.GetComponentInChildren<Text>().text = tempHeadData.strDoctorName;
            m_BtnRefData.GetComponentInChildren<Text>().alignment = TextAnchor.LowerCenter;
        }
        else
        {
            m_studyManager.SetCompairFileName(strFileName);
            bIsCompairFileSet = true;
            m_CompairData.SetActive(true);
            var tempHeadData = FileReader.GetHeadFromFile(strFileName);
            m_CompairData.GetComponentInChildren<Image>().overrideSprite = ToolFunction.CreateSpriteFromImage(tempHeadData.strPortraitPath);
            m_CompairData.GetComponentInChildren<Text>().text = tempHeadData.strDoctorName;
        }
    }
}
