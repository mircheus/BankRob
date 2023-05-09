using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vault : MonoBehaviour
{
    private Animator _animator;
    private int _openVault = Animator.StringToHash("OpenVault");
    
    public event UnityAction Robbed;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Robber robber))
        {
            Robbed?.Invoke();
            _animator.Play(_openVault);
            Debug.Log("Vault is robbed!");
        }
    }
}