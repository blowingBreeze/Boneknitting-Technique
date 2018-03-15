using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMovieList : MonoBehaviour
{
    public GameObject  m_ChooseList;

    // Update is called once per frame
    public void OnWakeUpList()
    {
        m_ChooseList.SetActive(true);
    }

    public void OnChooseMovie()
    {
        m_ChooseList.SetActive(false);
    }
}
