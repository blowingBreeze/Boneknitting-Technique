using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FDTGloveUltraCSharpWrapper;


public class DTGloveManager : MonoBehaviour {
    
    //public static DTGloveManager instance;
    CfdGlove glove;
    //Transform[] IHandbones = new Transform[15];
    public static bool caliFileLoaded;
    public static float[] DTvalues = new float[20];
    // Use this for initialization
    public bool InitDevice () {
        glove = new CfdGlove();
        glove.Open("USB0");
        caliFileLoaded = glove.LoadCalibration("Assets\\Cal\\right.cal");
        return true;
    }
    public void DisconnectDevice() {
        if (glove.IsOpen())
        {
            glove.Close();
        }
    }

    // Update is called once per frame
    public void AcquireHandData()
    {
        glove.GetSensorScaledAll(ref DTvalues);
    }

    void Start()
    {
        //if (InitDevice() == true)
        //{
        //    glove = new CfdGlove();
        //    glove.Open("USB0");
        //    caliFileLoaded = glove.LoadCalibration("Assets\\Cal\\right.cal");
        //}
        //instance = this;
        InitDevice();
    }

    //private void Update()
    //{
    //    AcquireHandData();
    //}
}
