using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private RobStarter _robStarter;
    private CinemachineVirtualCamera _camera;
    private Transform _followRobber;
    private float _freezeY;
    

    private void Start()
    {

        // _freezeY = transform.position.x;
    }
    
}
