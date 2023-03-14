using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WallCrusher : MonoBehaviour
{
    private int _damage;
    private Wall _wallToCrush;

    public event UnityAction WallCollided;
    public event UnityAction WallDestroyed;

    // public Wall WallToCrush => _wallToCrush;
    
    private void Start()
    {
        _damage = GetComponent<Robber>().Level + 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Wall wall))
        {
            _wallToCrush = wall;
            _wallToCrush.Destroyed += OnWallDestroyed;
            WallCollided?.Invoke();
        }
    }

    public void Attack()
    {
        _wallToCrush.ApplyDamage(_damage);
    }

    public void IncreaseDamage(int level)
    {
        _damage *= level;
    }

    public void OnWallDestroyed()
    {
        WallDestroyed?.Invoke();
    }
}
