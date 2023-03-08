using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WallCrusher : MonoBehaviour
{
    public event UnityAction<Wall> WallCollided;
    private int _damage;
    private Wall _wallToCrush;

    public Wall WallToCrush => _wallToCrush;
    
    private void Start()
    {
        _damage = GetComponent<Robber>().Level + 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Wall wall))
        {
            _wallToCrush = wall;
            WallCollided?.Invoke(wall);
        }
    }

    public void Attack()
    {
        // wall.ApplyDamage(_damage);
        _wallToCrush.ApplyDamage(_damage);
        Debug.Log($"damage = {_damage}");
    }

    public void IncreaseDamage(int level)
    {
        _damage *= level;
    }
}
