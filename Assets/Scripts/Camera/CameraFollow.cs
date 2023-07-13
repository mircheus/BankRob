using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private RobStarter _robStarter;
    [SerializeField] private TargetMovement _targetMovement;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private float _smoothTime;
    [SerializeField] private float _maxFov;
    [SerializeField] private float _minFov;
    [SerializeField] private float _startFov;

    private Vector3 _velocity = Vector3.zero;
    private Transform _followRobber;
    private float _freezeY;
    private bool _isFollowing;

    private void OnEnable()
    {
        _robStarter.Started += OnStarted;
    }

    private void OnDisable()
    {
        _robStarter.Started -= OnStarted;
    }

    private void Start()
    {
        _isFollowing = false;
        _camera.m_Lens.FieldOfView = _startFov;
    }

    private void Update()
    {
        if (_isFollowing)
        {
            MoveCamera();
            KeepCameraOffset(_targetMovement.DeltaY);
        }
    }

    private void OnStarted()
    {
        _isFollowing = true;
    }

    private void MoveCamera()
    {
        Vector3 position = Vector3.SmoothDamp(transform.position, _targetMovement.transform.position, ref _velocity,
            _smoothTime);
        transform.position = new Vector3(transform.position.x, position.y, transform.position.z);
    }

    private void KeepCameraOffset(float deltaY)
    {
        deltaY /= 100;
        _camera.m_Lens.FieldOfView = Mathf.Lerp(_minFov, _maxFov, deltaY);
    }
}
