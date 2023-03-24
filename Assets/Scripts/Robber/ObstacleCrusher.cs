using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleCrusher : MonoBehaviour
{
    private int _damage;
    private Obstacle _obstacleToCrush;

    public event UnityAction ObstacleCollided;
    public event UnityAction ObstacleDestroyed;

    // public Wall WallToCrush => _wallToCrush;
    
    private void Start()
    {
        _damage = GetComponent<Robber>().Level;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle wall))
        {
            _obstacleToCrush = wall;
            _obstacleToCrush.Destroyed += OnObstacleDestroyed;
            ObstacleCollided?.Invoke();
        }
    }

    public void Attack()
    {
        _obstacleToCrush.ApplyDamage(_damage);
    }

    public void IncreaseDamage(int level)
    {
        _damage *= level;
    }

    public void OnObstacleDestroyed()
    {
        ObstacleDestroyed?.Invoke();
        _obstacleToCrush.Destroyed -= OnObstacleDestroyed;
    }
}
