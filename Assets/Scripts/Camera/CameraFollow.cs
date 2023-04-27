using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private RobStarter _robStarter;
    [SerializeField] private GameObject _cameraTarget;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private float _currentVelocity;
    [SerializeField] private float _smoothTime;

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
    }

    private void Update()
    {
        if (_isFollowing)
        {
            MoveCamera();
        }
    }

    private void OnStarted()
    {
        _isFollowing = true;
    }

    private void MoveCamera()
    {
        // Vector3 position = Vector3.Lerp(transform.position, _cameraTarget.transform.position,
        //     _currentVelocity * Time.deltaTime);
        Vector3 position = Vector3.SmoothDamp(transform.position, _cameraTarget.transform.position, ref _velocity,
            _smoothTime);
        transform.position = new Vector3(transform.position.x, position.y, transform.position.z);
    }
}
