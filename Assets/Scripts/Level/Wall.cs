using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class Wall : MonoBehaviour
{
    [SerializeField] private int _health;
    
    public event UnityAction Destroyed;

    private void OnEnable()
    {
        Destroyed += DisableWall;
    }

    private void OnDisable()
    {
        Destroyed -= DisableWall;
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        
        if (_health <= 0)
        {
            Destroyed?.Invoke();
        }
    }

    private void DisableWall()
    {
        gameObject.SetActive(false);
    }
}
