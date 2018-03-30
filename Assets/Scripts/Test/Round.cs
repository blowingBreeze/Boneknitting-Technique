using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour {
    private Transform m_Parent;
    public float  m_Angle;
	// Use this for initialization
	void Start ()
    {
        m_Parent = transform.parent;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.RotateAround(m_Parent.transform.position, new Vector3(1, 1, 1), m_Angle);
	}
}
