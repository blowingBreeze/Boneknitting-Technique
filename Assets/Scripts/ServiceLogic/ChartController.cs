using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartController : MonoBehaviour
{
    public LineChart Speed;
    public LineChart SpeedRef;

    public LineChart Accelerate;
    public LineChart AccelerateRef;

    public LineChart Curvature;
    public LineChart CurvatureRef;

    public LineChart Torsion;
    public LineChart TorsionRef;
    // Use this for initialization
    void Start()
    {
        SpeedRef.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
