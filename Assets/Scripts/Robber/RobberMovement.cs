using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.Events;

public class RobberMovement : MonoBehaviour
{
    [SerializeField] private ObstacleCrusher _obstacleCrusher;
    [SerializeField] float _defaultSpeed = 5f;
    
    [Header("Debug")]
    [SerializeField] private Transform _currentTarget;
    [SerializeField] private Transform[] _runAwayTargets;
    [SerializeField] private Transform _runAwayTarget;
    [SerializeField] private Transform _runAwayTarget2;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _increasedSpeed;
    [SerializeField] private int _runAwayTargetCounter = 0;

    private Transform _downTarget;
    private Obstacle _currentObstacle;
    private bool _isShieldActive = false;
    private bool _isDashActive = false;
    private bool _isGetStopped = false;
    private bool _isRotated = false;

    public bool IsDashActive => _isDashActive;
    public bool IsGetStopped => _isGetStopped;
    public bool IsShieldActive => _isShieldActive;
    public float IncreasedSpeed => _increasedSpeed;
    
    public event UnityAction GetStopped;
    public event UnityAction ReachedVault;

    private void OnEnable()
    {
        _obstacleCrusher.ObstacleCollided += StopMoving;
        _obstacleCrusher.ObstacleDestroyed += StartMoving;
        GetStopped += StopMoving;
        GetStopped += OnGetStopped;
    }

    private void OnDisable()
    {
        _obstacleCrusher.ObstacleCollided -= StopMoving;
        _obstacleCrusher.ObstacleDestroyed -= StartMoving;
        GetStopped -= StopMoving;
        GetStopped -= OnGetStopped;
    }

    private void Start()
    {
        _currentTarget = _downTarget;
        _currentSpeed = _defaultSpeed;
    }
    
    private void Update()
    {
        MoveTo(_currentTarget.position);
    }
    
    public void SetTarget(Transform target)
    {
        _downTarget = target;
        _currentTarget = target;
    }

    public void GetTrappedBy(Trap trap)
    {
        if (trap.gameObject.TryGetComponent(out Cage cage))
        {
            if (_isDashActive == false)
            {
                GetStopped?.Invoke();
            }
        }

        if (trap.gameObject.TryGetComponent(out Dynamite dynamite))
        {
            if (_isShieldActive == false)
            {
                GetStopped?.Invoke();
            }
        }
    }
    
    public void SetIncreasedSpeedBy(DashPerk perk)
    {
        _currentSpeed = _increasedSpeed;
        _isDashActive = true;
    }

    public void SetDefaultSpeedBy(DashPerk perk)
    {
        _currentSpeed = _defaultSpeed;
        _isDashActive = false;
    }

    public void SetShieldActive(bool value)
    {
        _isShieldActive = value;
    }

    public void SetRunAwayTargets(Transform[] runAwayTargets)
    {
        _runAwayTargets = runAwayTargets;
        ReachedVault?.Invoke();
    }

    private void MoveTo(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, _currentSpeed * Time.deltaTime);
            
        if (Math.Abs(transform.position.y - target.y) < .01f)
        {
            SetNextRunAwayTarget();
        }
    }
    
    private void StopMoving()
    {
        _currentSpeed = 0f;
    }

    private void StartMoving()
    {
        _currentSpeed = _defaultSpeed;
        
        if (_isDashActive)
        {
            _currentSpeed = _increasedSpeed;
        }
    }
    
    private void OnGetStopped()
    {
        _isGetStopped = true;
    }

    private void SetNextRunAwayTarget()
    {
        
        if (_runAwayTargetCounter < _runAwayTargets.Length)
        {
            _currentTarget = _runAwayTargets[_runAwayTargetCounter];
            _runAwayTargetCounter++;
        }
    }

    public void RotateRight()
    {
        // transform.rotation.SetLookRotation(Vector3.right, Vector3.up);
        // transform.rotation = Vector3.rot
        if (_isRotated == false)
        {
            transform.Rotate(Vector3.up, 90f);
            _isRotated = true;
        }
    }
}
