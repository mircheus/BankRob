using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberMovement : MonoBehaviour
{
    [SerializeField] private WallCrusher _wallCrusher;
    [SerializeField] float _fallingSpeed = 5f;
    [SerializeField] private Transform _downTarget;
    private Vector3 _upOffsetVector = new Vector3(0, .5f, 0);
    private Vector3 _currentTarget;
    private Wall _currentWall;
    private float _currentSpeed;
    
    
    private void OnEnable()
    {
        // _wallCrusher.WallCollided += SetTarget;
        _wallCrusher.WallCollided += StopMoving;
    }

    private void OnDisable()
    {
        // _wallCrusher.WallCollided -= SetTarget;
        _wallCrusher.WallCollided -= StopMoving;
    }

    private void Start()
    {
        _currentTarget = _downTarget.position;
        _currentSpeed = _fallingSpeed;
    }
    
    private void Update()
    {
        MoveIn(_currentTarget);
    }
    
    private void MoveIn(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, _currentSpeed * Time.deltaTime);

        if (Math.Abs(transform.position.y - target.y) < .1f)
        {
            _currentTarget = _downTarget.position;
        }
    }

    private void SetTarget()
    {
        _currentTarget = transform.position + _upOffsetVector;
    }

    private void StopMoving(Wall wall)
    {
        _currentSpeed = 0f;
        _currentWall = wall;
        _currentWall.Destroyed += StartMoving;
    }

    private void StartMoving()
    {
        _currentSpeed = _fallingSpeed;
        _currentWall.Destroyed -= StartMoving;
    }
}
