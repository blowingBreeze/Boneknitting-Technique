using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
        ConfigCenter.GetConfigCenterInstance().ConfigDataInit(DataPath.strConfigFilePath);
        Application.targetFrameRate = 30;
    }


}
