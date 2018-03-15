using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpMovieList : MonoBehaviour
{
    public RectTransform m_MovieListTranform;

    public void OnPointerEnter()
    {
        m_MovieListTranform.Translate(new Vector3(-m_MovieListTranform.sizeDelta.x, 0, 0));
    }
}
