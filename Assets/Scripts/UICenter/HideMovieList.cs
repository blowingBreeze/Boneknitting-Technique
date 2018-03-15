using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMovieList : MonoBehaviour
{
    public void OnPointerExit()
    {
        var m_MovieListTranform = GetComponent<RectTransform>();
        m_MovieListTranform.Translate(new Vector3(m_MovieListTranform.sizeDelta.x, 0, 0));
    }
}
