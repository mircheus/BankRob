using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Progression : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    public event UnityAction LevelParametersPrepared;
    
    private int _levelsCounter;
    private int _floorsQuantity;
    private int _obstaclesLevel;

    public int ObstaclesLevel => _obstaclesLevel;
    public int FloorsQuantity => _floorsQuantity;

    private void OnEnable()
    {
        _playerData.DataLoaded += OnDataLoaded;
    }

    private void OnDisable()
    {
        _playerData.DataLoaded -= OnDataLoaded;
    }

    private void OnDataLoaded()
    {
        PrepareLevelParameters();
        LevelParametersPrepared?.Invoke();
    }

    private void PrepareLevelParameters()
    {
        int levelsPassed = _playerData.CompletedLevelsCounter;
        _floorsQuantity = CalculateFloorsQuantity(levelsPassed);
        _obstaclesLevel = SetObstaclesLevel(levelsPassed);
    }
    
    private int CalculateFloorsQuantity(int levelsPassed) // DRAFT mechanic of progression
    {
        if (levelsPassed < 3)
        {
            return 3;
        }
        else if (levelsPassed >= 3 && levelsPassed < 6 )
        {
            return 4;
        }
        else if (levelsPassed >= 6 && levelsPassed < 10)
        {
            return 5;
        }
        else if (levelsPassed >= 10 && levelsPassed < 15)
        {
            return 6;
        }

        return 404;
    }

    private int SetObstaclesLevel(int levelsPassed) // DRAFT mechanic of progression
    {
        if (levelsPassed < 3)
        {
            return 1;
        }
        else if (levelsPassed >= 3 && levelsPassed < 6 )
        {
            return 2;
        }
        else if (levelsPassed >= 6 && levelsPassed < 10)
        {
            return 3;
        }
        else if (levelsPassed >= 10 && levelsPassed < 15)
        {
            return 4;
        }

        return -1;
    }
}
