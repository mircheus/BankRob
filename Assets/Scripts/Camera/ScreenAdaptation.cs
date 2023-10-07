using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[ExecuteInEditMode]
public class ScreenAdaptation : MonoBehaviour
{
    [SerializeField] private Vector3 _mobileDistance;
    [SerializeField] private Vector3 _desktopDistance;
    [SerializeField] private float _edgeValue;

    private void Update()
    {
        SetDistanceByAspectRation();
    }

    private void SetDistanceByAspectRation()
    {
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        
        if (aspectRatio < _edgeValue)
        {
            transform.position = _mobileDistance;
        }
        else if (aspectRatio > _edgeValue)
        {
            transform.position = _desktopDistance;
        }
    }
}
