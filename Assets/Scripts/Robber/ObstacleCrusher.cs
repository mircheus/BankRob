using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleCrusher : MonoBehaviour
{
    [SerializeField] private int _damageLevelUpgrade;
    [Header("DEBUG")]
    [SerializeField] private int _defaultDamage;

    private int _currentDamage;
    private Obstacle _obstacleToCrush;

    public event UnityAction ObstacleCollided;
    public event UnityAction ObstacleDestroyed;
    public event UnityAction Attacked;

    public Obstacle ObstacleToCrush => _obstacleToCrush;
    
    private void Start()
    {
        _defaultDamage = GetComponent<Robber>().Level + 1;
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
        }
    }

    public void Attack()
    {
        _obstacleToCrush.ApplyDamage(_currentDamage);
        Attacked?.Invoke();
    }

    public void IncreaseDamage()
    {
        _defaultDamage += _damageLevelUpgrade;
        _currentDamage = _defaultDamage;
    }

    private void OnObstacleDestroyed()
    {
        _obstacleToCrush.Destroyed -= OnObstacleDestroyed;
        ObstacleDestroyed?.Invoke();
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
