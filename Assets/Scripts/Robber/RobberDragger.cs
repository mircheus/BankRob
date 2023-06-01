using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberDragger : MonoBehaviour, IDrag
{
    private Rigidbody _rigidbody;
    private bool _isDraggingNow;
    private Transform _lastParent;
    private Vector3 _offset;

    public bool IsDraggingNow => _isDraggingNow;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _isDraggingNow = false;
    }

    public void OnStartDrag()
    {
        _isDraggingNow = true;
    }

    public void OnEndDrag()
    {
        _isDraggingNow = false;
        _rigidbody.velocity = Vector3.zero;
        transform.position = _lastParent.transform.position + _offset;
    }

    public void SetLastParentTransform(Transform transform)
    {
        _lastParent = transform;
    }

    public void SetOffset(Vector3 offsetVector)
    {
        _offset = offsetVector;
    }
}
