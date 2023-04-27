using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ScreenAdaptation : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Vector3 _mobileDistance;
    [SerializeField] private Vector3 _desktopDistance;
    [SerializeField] private int _mobileHeight;
    [SerializeField] private int _desktopHeight;

    private void Start()
    {
        if (Screen.height == _mobileHeight)
        {
            transform.position = _mobileDistance;
            Debug.Log(Screen.height);
        }
        else if (Screen.height == _desktopHeight)
        {
            transform.position = _desktopDistance;
            Debug.Log(Screen.height);
        }
    }
}
