using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginRecord : MonoBehaviour
{
    public GameObject StopRecord;
    public void OnClick()
    {
        StopRecord.SetActive(true);
        Destroy(gameObject);
    }

}
