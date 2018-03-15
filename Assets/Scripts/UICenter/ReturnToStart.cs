using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToStart : MonoBehaviour {

    public GameObject StartCanvas;
	// Use this for initialization
	public void OnReturn()
    {
        Instantiate(StartCanvas);
        Destroy(gameObject);
		
	}
}
