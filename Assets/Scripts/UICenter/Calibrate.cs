using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibrate : MonoBehaviour
{
    public GameObject BeginRecord;

    public void OnClick()
    {
        BeginRecord.SetActive(true);
        Destroy(gameObject);
    }

}
