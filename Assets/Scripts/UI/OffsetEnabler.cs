using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using LayerLab;
using UnityEngine;

public class OffsetEnabler : MonoBehaviour
{
    [SerializeField] private GameObject _preparingMenu;
    [SerializeField] private int _yOffset;
    
    private void Start()
    {
        float aspectRatio = GetAspectRatio();
        // Debug.Log(aspectRatio);
        
        if (aspectRatio < 1)
        {
            _preparingMenu.transform.position += new Vector3(0, _yOffset, 0);
        }
    }
    
    private float GetAspectRatio()
    {
        return Screen.width/ Screen.height;
    }
}
