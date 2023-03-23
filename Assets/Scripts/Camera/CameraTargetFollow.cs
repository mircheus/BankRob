using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetFollow : MonoBehaviour
{
    [SerializeField] private RobStarter _robStarter;
    
    private Robber _robber;

    private void OnEnable()
    {
        _robStarter.Started += OnRobStarterStarted;
    }

    private void OnDisable()
    {
        _robStarter.Started -= OnRobStarterStarted;
    }
    
    private void Update()
    {
        if (_robber != null)
        {
            Follow();
        }
    }

    private void Follow()
    {
        var transform1 = transform;
        var position = transform1.position;
        position = new Vector3(position.x, _robber.transform.position.y, position.z);
        transform1.position = position;
    }
    
    private void OnRobStarterStarted()
    {
        _robber = _robStarter.PickRobber();
    }
}
