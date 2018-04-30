﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyModeManager : MonoBehaviour
{

    private string m_strRefFileName; //参照数据文件路径
    private string m_strFileName; //需对比的文件路径
    private bool bIsRecord;
    private bool bIsStart;
    private float fTimeCount;

    private GameObject modelRef;
    private GameObject model;
    private StudyController m_StudyController;

    // Use this for initialization
    void Awake()
    {
        m_strRefFileName = null;
        m_strFileName = null;
        bIsRecord = false;
        bIsStart = false;
        fTimeCount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(bIsStart)
        {
            m_StudyController.Update(fTimeCount, fTimeCount);
        }
    }

    public void SetRefFileName(string strFileName)
    {
        if(string.IsNullOrEmpty(strFileName))
        {
            return;
        }
        m_strRefFileName =strFileName;
    }

    public void SetCompairFileName(string strFileName)
    {
        if (string.IsNullOrEmpty(strFileName))
        {
            return;
        }
        m_strFileName = strFileName;
    }

    public void SetRecordAsTrue()
    {
        bIsRecord = true;
    }

    public void Prepare()
    {
        if (bIsRecord)
        {
            m_StudyController = new StudyControllerFileRecord(modelRef, model, m_strRefFileName);
        }
        else
        {
            m_StudyController = new StudyControllerFileFile(modelRef, model, m_strRefFileName, m_strFileName);
        }
    }

    public void StartStudy()
    {
        m_StudyController.Ready();
        bIsStart = !bIsStart;
    }
}
