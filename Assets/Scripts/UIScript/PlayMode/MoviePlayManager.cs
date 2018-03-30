using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoviePlayManager : MonoBehaviour
{
    private PlayController m_PlayController;  

    private void Awake()
    {
        m_PlayController = new PlayController();
    }

    private void Update()
    {
        
    }

    public PlayController GetPlayController()
    {
        return m_PlayController;
    }
}
