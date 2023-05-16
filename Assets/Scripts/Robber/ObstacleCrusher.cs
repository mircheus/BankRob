using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleCrusher : MonoBehaviour
{
    [Header("DEBUG")]
    [SerializeField] private int _defaultDamage;

    private int _currentDamage;
    private Obstacle _obstacleToCrush;

    public event UnityAction ObstacleCollided;
    public event UnityAction ObstacleDestroyed;

    public Obstacle ObstacleToCrush => _obstacleToCrush;
    
    private void Start()
    {
        _defaultDamage = GetComponent<Robber>().Level;
        _currentDamage = _defaultDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            _obstacleToCrush = obstacle;
            _obstacleToCrush.Destroyed += OnObstacleDestroyed;
            ObstacleCollided?.Invoke();
            Attack();
            // Attack();
        }
    }

    public void Attack()
    {
        _obstacleToCrush.ApplyDamage(_currentDamage);
    }

    public void IncreaseDamage(int level)
    {
        _defaultDamage += 2;
        _currentDamage = _defaultDamage;
        // Debug.Log($"currentDamage: {_damage}");
    }

    public void OnObstacleDestroyed(Vector3 position)
    {
        ObstacleDestroyed?.Invoke();
        _obstacleToCrush.Destroyed -= OnObstacleDestroyed;
        _obstacleToCrush = null;
    }

    public void IncreaseDamageBySpeedPerk(DashPerk perk, int increasedDamage)
    {
        _currentDamage = increasedDamage;
    }

    public void SetDefaultDamageBySpeedPerk(DashPerk perk)
    {
        _currentDamage = _defaultDamage;
    }
}
