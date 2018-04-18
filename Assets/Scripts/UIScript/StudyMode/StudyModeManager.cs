using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyModeManager : MonoBehaviour
{

    private string m_strRefFileName; //参照数据文件路径
    private string m_strFileName; //需对比的文件路径
    private bool bIsRecord;
    private bool bIsStart;
    // Use this for initialization
    void Start()
    {
        m_strRefFileName = null;
        m_strFileName = null;
        bIsRecord = false;
        bIsStart = false;
    }

    // Update is called once per frame
    void Update()
    {

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

    public void StartOrStop()
    {
        bIsStart = !bIsStart;
    }
}
