using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[ExecuteInEditMode]
public class ScreenAdaptation : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Vector3 _mobileDistance;
    [SerializeField] private Vector3 _desktopDistance;
    [SerializeField] private int _mobileHeight;
    [SerializeField] private int _desktopHeight;
    

    private void Start()
    {
        // SetCameraDistance();
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        Debug.Log(aspectRatio);
        Debug.Log($"width = {Screen.width}");
        Debug.Log($"height= {Screen.height}");
        SetDistanceBy(aspectRatio);
    }

    private void Update()
    {
        //     SetCameraDistance();
        // }
    }

    private void SetDistanceBy(float aspectRatio)
    {
        if (aspectRatio < 1)
        {
            transform.position = _mobileDistance;
        }
        else if (aspectRatio > 1)
        {
            transform.position = _desktopDistance;
        }
    }

    private void SetCameraDistance()
    {
        if (Screen.height >= _mobileHeight)
        {
            transform.position = _mobileDistance;
        }
        else if (Screen.height == _desktopHeight)
        {
            transform.position = _desktopDistance;
        }
        
        Debug.Log(Screen.height);
    }
}
