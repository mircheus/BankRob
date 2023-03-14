using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberMovement : MonoBehaviour
{
    [SerializeField] private WallCrusher _wallCrusher;
    [SerializeField] float _fallingSpeed = 5f;
    [SerializeField] private Vector3 _currentTarget;
    private Transform _downTarget;
    private Wall _currentWall;
    private float _currentSpeed;
    
    
    private void OnEnable()
    {
        _wallCrusher.WallCollided += StopMoving;
    }

    private void OnDisable()
    {
        _wallCrusher.WallCollided -= StopMoving;
    }

    private void Start()
    {
        _currentTarget = _downTarget.position;
        _currentSpeed = _fallingSpeed;
    }
    
    private void Update()
    {
        MoveTo(_currentTarget);
    }

    public void SetDownTarget(Transform target)
    {
        _downTarget = target;
        _currentTarget = target.position;
    }
    
    private void MoveTo(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, _currentSpeed * Time.deltaTime);

        if (Math.Abs(transform.position.y - target.y) < .1f)
        {
            _currentTarget = _downTarget.position;
        }
    }
    
    private void StopMoving()
    {
        _currentSpeed = 0f;
        // _currentWall = wall;
        _wallCrusher.WallDestroyed += StartMoving;
    }

    private void StartMoving()
    {
        _currentSpeed = _fallingSpeed;
        _wallCrusher.WallDestroyed -= StartMoving;
    }
}
