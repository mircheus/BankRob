using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberDragger : MonoBehaviour, IDrag
{
    private Rigidbody _rigidbody;
    private bool _isDraggingNow = false;
    private Transform _lastParent;

    public bool IsDraggingNow => _isDraggingNow;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnStartDrag()
    {
        _isDraggingNow = true;
    }

    public void OnEndDrag()
    {
        _isDraggingNow = false;
        _rigidbody.velocity = Vector3.zero;
        transform.position = _lastParent.transform.position;
    }

    public void SetLastParentTransform(Transform transform)
    {
        _lastParent = transform;
    }
}
