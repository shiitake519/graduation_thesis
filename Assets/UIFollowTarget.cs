using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIFollowTarget : MonoBehaviour 
{
    RectTransform rectTransform = null;
    //[SerializeField] Transform target = null;
    public ray r;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform> ();
    }

    void Update()
    {
        rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, r.posnum);
    }
}