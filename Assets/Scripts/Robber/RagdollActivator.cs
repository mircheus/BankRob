using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollActivator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody[] _allRigidbodies;
    [SerializeField] private Rigidbody _pelvisRigidbody;

    private bool _ragdollIsActive = false;

    private void Start()
    {
        DeactivateRagdoll();
    }
    
    public void ActivateRagdoll()
    {
        _ragdollIsActive = true;
        _animator.enabled = false;

        foreach (Rigidbody rigidbody in _allRigidbodies)
        {
            rigidbody.isKinematic = false;
        }

        _pelvisRigidbody.isKinematic = true;
    }

    public void DeactivateRagdoll()
    {
        _ragdollIsActive = false;
        _animator.enabled = true;
        
        foreach (Rigidbody rigidbody in _allRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
    }
}
