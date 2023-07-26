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
    [SerializeField] private AspectRatioChecker _aspectRatioChecker;
    
    private void Start()
    {
        float aspectRatio = _aspectRatioChecker.AspectRatio;

        if (aspectRatio < 1)
        {
            _preparingMenu.transform.position += new Vector3(0, _yOffset, 0);
        }
    }
}
