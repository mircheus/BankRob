using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int _health;
    
    public event UnityAction Destroyed;

    private void OnEnable()
    {
        Destroyed += DisableSelf;
    }

    private void OnDisable()
    {
        Destroyed -= DisableSelf;
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        
        if (_health <= 0)
        {
            Destroyed?.Invoke();
        }
    }

    private void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
