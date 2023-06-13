using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberDragger : MonoBehaviour, IDrag
{
    [SerializeField] private RagdollActivator _ragdollActivator;
    
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
        _ragdollActivator.ActivateRagdoll();
    }

    public void OnEndDrag()
    {
        _ragdollActivator.DeactivateRagdoll();
        _isDraggingNow = false;
        _rigidbody.velocity = Vector3.zero;

        if (gameObject.activeSelf)
        {
            transform.position = _lastParent.transform.position + _offset;
        }
    }

    public void SetLastParentTransform(Transform transform)
    {
        _lastParent = transform;
    }

    public void SetOffset(Vector3 offsetVector)
    {
        _offset = offsetVector;
    }

    public void DeleteLastParentTransform()
    {
        _lastParent = null;
    }
}
