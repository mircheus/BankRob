using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robber : MonoBehaviour, IDrag
{
    private Rigidbody _rigidbody;
    private bool _isDraggingNow = false;
    private Transform _parentSlot;

    public bool IsDraggingNow => _isDraggingNow;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            collision.gameObject.SetActive(false);
        }
    }

    public void OnStartDrag()
    {
        Debug.Log("Start dragging");
        _isDraggingNow = true;
    }

    public void OnEndDrag()
    {
        _isDraggingNow = false;
        _rigidbody.velocity = Vector3.zero;
    }
}
