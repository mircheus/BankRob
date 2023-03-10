using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vault : MonoBehaviour
{
    public event UnityAction Robbed;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Robber robber))
        {
            Robbed?.Invoke();
            Debug.Log("Vault is robbed!");
        }
    }
}
