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
    [SerializeField] private float _currentVelocity;
    [SerializeField] private float _smoothTime;
    [SerializeField] private float _zMax;
    [SerializeField] private float _zMin;
    [SerializeField] private float _maxFov;
    [SerializeField] private float _minFov;

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
            KeepCameraOffset(_targetMovement.DeltaY);
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
        Vector3 position = Vector3.SmoothDamp(transform.position, _targetMovement.transform.position, ref _velocity,
            _smoothTime);
        
        // Vector3 offset = Vector3.SmoothDamp(transform.position, );
        // float zOffset = CalculateOffsetByZ(transform.position.z);
        transform.position = new Vector3(transform.position.x, position.y, transform.position.z);
        // transform.position = new Vector3(transform.position.x, position.y, zOffset);
    }

    private void KeepCameraOffset(float deltaY)
    {
        if (deltaY > 10)
        {
            // _camera.m_Lens.FieldOfView = 60;
            DOTween.To(() => _camera.m_Lens.FieldOfView, x => _camera.m_Lens.FieldOfView = x, 50, 1);
        }
        
        if (deltaY < 10)
        {
            // _camera.m_Lens.FieldOfView = 60;
            DOTween.To(() => _camera.m_Lens.FieldOfView, x => _camera.m_Lens.FieldOfView = x, 40, 1);
        }
        
        if (deltaY > 30)
        {
            // _camera.m_Lens.FieldOfView = 80;
            // DOTween.To(()=> myVector, x=> myVector = x, new Vector3(3,4,8), 1);
            DOTween.To(() => _camera.m_Lens.FieldOfView, x => _camera.m_Lens.FieldOfView = x, 60, 1);
        }
        
        if (deltaY >= 10 && deltaY < 30 )
        {
            // _camera.m_Lens.FieldOfView = 80;
            // DOTween.To(()=> myVector, x=> myVector = x, new Vector3(3,4,8), 1);
            DOTween.To(() => _camera.m_Lens.FieldOfView, x => _camera.m_Lens.FieldOfView = x, 60, 1);
        }
        
        if (deltaY > 50)
        {
            _camera.m_Lens.FieldOfView = 100;
            DOTween.To(() => _camera.m_Lens.FieldOfView, x => _camera.m_Lens.FieldOfView = x, 70, 1);
        }
    }

    private float CalculateOffsetByZ(float z)
    {
        float result = z + (_targetMovement.DeltaY / 100f);

        if (result < _zMin)
        {
            return _zMin;
        }

        if (result > _zMax)
        {
            return _zMax;
        }
        
        return result;
    }
}
