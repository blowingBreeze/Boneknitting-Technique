using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public Transform FallowObj;
    public Vector3 hight;
    private void Start()
    {
        FallowObj = GameObject.FindGameObjectWithTag("Hand").transform;
    }

    private void Update()
    {
        transform.position = FallowObj.position + hight;
        transform.forward = FallowObj.position - transform.position; 
    }
}
