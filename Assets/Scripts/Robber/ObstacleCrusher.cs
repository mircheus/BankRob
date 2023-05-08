using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleCrusher : MonoBehaviour
{
    [Header("DEBUG")]
    [SerializeField] private int _damage;
    private Obstacle _obstacleToCrush;

    public event UnityAction ObstacleCollided;
    public event UnityAction ObstacleDestroyed;

    private void Start()
    {
        _damage = GetComponent<Robber>().Level;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            _obstacleToCrush = obstacle;
            _obstacleToCrush.Destroyed += OnObstacleDestroyed;
            ObstacleCollided?.Invoke();
            // Attack();
        }
    }

    public void Attack()
    {
        _obstacleToCrush.ApplyDamage(_damage);
    }

    public void IncreaseDamage(int level)
    {
        _damage += 2;
        // Debug.Log($"currentDamage: {_damage}");
    }

    public void OnObstacleDestroyed(Vector3 position)
    {
        ObstacleDestroyed?.Invoke();
        _obstacleToCrush.Destroyed -= OnObstacleDestroyed;
    }
}
