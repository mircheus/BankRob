using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Progression : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Robbery _robbery;

    public event UnityAction LevelParametersPrepared;
    
    private int _levelsCounter;
    private int _floorsQuantity;
    private int _obstaclesLevel;
    private int _keysQuantity;
    private int _keysFromPreviousLevel;
    private int _trapsLevel;
    private int _trapsQuantity;

    public int FloorsQuantity => _floorsQuantity;
    public int ObstaclesLevel => _obstaclesLevel;
    public int KeysQuantity => _keysQuantity;
    public int KeysFromPreviousLevel => _keysFromPreviousLevel;
    public int TrapsQuantity => _trapsQuantity;
    public int TrapsLevel => _trapsLevel;

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
        _keysQuantity = SetKeysQuantity(levelsPassed);
        _trapsLevel = SetTrapsLevel(levelsPassed);
        _trapsQuantity = SetTrapsQuantity(levelsPassed);
        _keysFromPreviousLevel = _playerData.KeysAmount;
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
        else if (levelsPassed >= 10)
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
        else if (levelsPassed >= 10)
        {
            return 4;
        }

        return -1;
    }

    private int SetKeysQuantity(int levelsPassed) // DRAFT mechanic of progression
    {
        if (levelsPassed >= 3)
        {
            return 2;
        }

        return 0;
    }

    private int SetTrapsQuantity(int levelsPassed)
    {
        if (levelsPassed >= 3)
        {
            return 2;
        }

        return 0;
    }

    private int SetTrapsLevel(int levelsPassed)
    {
        if (levelsPassed < 3)
        {
            return 0;
        }

        if (levelsPassed >= 3 && levelsPassed <= 10)
        {
            return 1;
        }

        return 0;
    }
}
