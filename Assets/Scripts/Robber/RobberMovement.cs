using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberMovement : MonoBehaviour
{
    [SerializeField] private ObstacleCrusher _obstacleCrusher;
    [SerializeField] float _fallingSpeed = 5f;
    
    [Header("Debug")]
    [SerializeField] private Vector3 _currentTarget;
    
    private Transform _downTarget;
    private Obstacle _currentObstacle;
    private float _currentSpeed;

    private void OnEnable()
    {
        _obstacleCrusher.ObstacleCollided += StopMoving;
    }

    private void OnDisable()
    {
        _obstacleCrusher.ObstacleCollided -= StopMoving;
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
        _obstacleCrusher.ObstacleDestroyed += StartMoving;
    }

    private void StartMoving()
    {
        _currentSpeed = _fallingSpeed;
        _obstacleCrusher.ObstacleDestroyed -= StartMoving;
    }
}
