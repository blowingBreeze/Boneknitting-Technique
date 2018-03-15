using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public GameObject SaveBtn;
    public void OnClick()
    {
        SaveBtn.SetActive(true);
        Destroy(gameObject);
    }

}
