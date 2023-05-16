using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RobberMovement : MonoBehaviour
{
    [SerializeField] private ObstacleCrusher _obstacleCrusher;
    [SerializeField] float _defaultSpeed = 5f;
    
    [Header("Debug")]
    [SerializeField] private Vector3 _currentTarget;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _tweakableSpeed;

    private Transform _downTarget;
    private Obstacle _currentObstacle;
    private bool _isShieldActive = false;
    private bool _isDashActive = false;

    public bool IsDashActive => _isDashActive;
    
    public event UnityAction GetStopped;

    private void OnEnable()
    {
        _obstacleCrusher.ObstacleCollided += StopMoving;
        _obstacleCrusher.ObstacleDestroyed += StartMoving;
        GetStopped += StopMoving;
    }

    private void OnDisable()
    {
        _obstacleCrusher.ObstacleCollided -= StopMoving;
        _obstacleCrusher.ObstacleDestroyed -= StartMoving;
        GetStopped -= StopMoving;
    }

    private void Start()
    {
        _currentTarget = _downTarget.position;
        _currentSpeed = _defaultSpeed;
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

    public void GetTrappedBy(Trap trap) 
    {
        if (trap.gameObject.TryGetComponent(out Cage cage))
        {
            if (_isDashActive == false)
            {
                GetStopped?.Invoke();
                Debug.Log("Stopped by cage");
            }
        }
        
        if (_isShieldActive == false)
        {
            GetStopped?.Invoke();
            Debug.Log("Get Trapped");
        }
    }
    
    
    
    public void SetIncreasedSpeedBy(DashPerk perk, float increasedSpeed)
    {
        _currentSpeed = increasedSpeed;
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
    }

    private void StartMoving()
    {
        _currentSpeed = _defaultSpeed;
    }
}
