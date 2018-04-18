using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePanel : MonoBehaviour
{
    public GameObject m_SavePanel;
    public GameObject m_InfoSaved;
    public DoubleEndSlider m_CutSlider;
    public InputField m_DoctorName;

    private RecordManager m_RecordManager;

    private string m_PortraitPath;
    private string m_FilePath;
    private float m_fLeftSliderValue;
    private float m_fRightSliderValue;
    private bool bIsLeft;

    private void Start()
    {
        m_CutSlider.eValueChange += CutSlider_OnValueChange;
        m_RecordManager = GetComponent<RecordManager>();

        m_PortraitPath = null;
        m_FilePath = null;
        m_fLeftSliderValue = 0.0f;
        m_fRightSliderValue = 1.0f;
        bIsLeft = false;
    }

    private void CutSlider_OnValueChange(float value, bool bIsLeft)
    {
        if (bIsLeft)
        {
            m_fLeftSliderValue = value;
        }
        else
        {
            m_fRightSliderValue = value;
        }
        float tempTimeCount = m_RecordManager.GetTimeCount();
        ModelCtrlData tempModelCtrlData = m_RecordManager.GetRecordController().GetModelCtrlDataByTime(tempTimeCount * value);
        m_RecordManager.GetRecordController().GetPlayController().Update(tempModelCtrlData);
    }

    public void Btn_Confirm()
    {
        m_SavePanel.SetActive(true);
        m_CutSlider.gameObject.SetActive(false);
    }

    public void PortraitPath()
    {
        string filter = "*.jpg;*.png";
        string title = "选择头像";
        string extension = "png";
        m_PortraitPath = ToolFunction.OpenFilePath(filter, title, extension);
        //TODO-将图片替换
    }

    public void Btn_Save()
    {
        if (string.IsNullOrEmpty(m_DoctorName.text))
        {
            m_DoctorName.placeholder.GetComponent<Text>().color = Color.red;
        }
        else
        {
            string filter = "*.txt";
            string title = "保存文件";
            string extension = "txt";
            m_FilePath = ToolFunction.SaveFilePath(filter, title, extension);

            MovieHeadData tempData = new MovieHeadData();
            tempData.strDoctorName = m_DoctorName.text;
            tempData.strPortraitPath = m_PortraitPath;
            float tempTimeCount = m_RecordManager.GetTimeCount();
            tempData.fCurrentTime = 0.0f;
            float tempStartTime = tempTimeCount * m_fLeftSliderValue;
            float tempEndTime = tempTimeCount * m_fRightSliderValue;
            tempData.fTotalTime = tempEndTime - tempStartTime;
            m_RecordManager.GetRecordController().SaveDataToFile(
                tempData,
                m_FilePath,
                tempStartTime,
                tempEndTime
                );

            m_SavePanel.SetActive(false);
            m_InfoSaved.SetActive(true);
        }

    }



}
