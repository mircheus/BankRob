using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Progression : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Robbery _robbery;

    [Header("Progression settings")] 
    [SerializeField] private int _difficultyFactor;
    [SerializeField] private int _obstaclesReducer;
    [SerializeField] private int _trapsAppearsOnLevel;

    [Header("Obstacles")] 
    [SerializeField] private Barrier[] _obstacles;

    [Header("Traps")] 
    [SerializeField] private Barrier[] _traps;

    [Header("First level values")] 
    [Header("Player settings")]
    [SerializeField] private int _initMoney;
    [Header("Level settings")]
    [SerializeField] private int _firstLevelFloorsAmount;

    public event UnityAction LevelParametersPrepared;
    public event UnityAction<Barrier[,]> LevelMapPrepared; 
    
    private int _levelsCounter;
    private int _floorsQuantity;
    private int _obstaclesQuantity;
    private int _obstaclesLevel;
    private int _keysQuantity;
    private int _keysFromPreviousLevel;
    private int _trapsLevel;
    private int _trapsQuantity;

    public int FloorsQuantity => _floorsQuantity;
    public int ObstaclesQuantity => _obstaclesQuantity;
    public int ObstaclesLevel => _obstaclesLevel;
    public int KeysQuantity => _keysQuantity;
    public int KeysFromPreviousLevel => _keysFromPreviousLevel;
    public int TrapsQuantity => _trapsQuantity;
    public int TrapsLevel => _trapsLevel;
    public int InitMoney => _initMoney;
    public int FirstLevelFloorsAmount => _firstLevelFloorsAmount;

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
        // PrepareLevelParameters();
        // LevelParametersPrepared?.Invoke();
        LevelMapPrepared?.Invoke(GetLevelMap());
    }

    private void PrepareLevelParameters()
    {
        int levelsPassed = _playerData.CompletedLevelsCounter;
        int floorsAmountFromPreviousLevel = _playerData.FloorsAmountFromPreviousLevel;
        
        _floorsQuantity = CalculateFloorsQuantity(levelsPassed, floorsAmountFromPreviousLevel);
        _trapsQuantity = CalculateTrapsQuantity(levelsPassed, _floorsQuantity);
        _obstaclesQuantity = CalculateObstaclesQuantity(_floorsQuantity, _trapsQuantity);
    }
    
    private Barrier[,] GetLevelMap() 
    {
        int levelsPassed = _playerData.CompletedLevelsCounter;
        int floorsAmountFromPreviousLevel = _playerData.FloorsAmountFromPreviousLevel;
        _floorsQuantity = CalculateFloorsQuantity(levelsPassed, floorsAmountFromPreviousLevel);
        Barrier[,] levelMap = new Barrier[_floorsQuantity - 1, 4]; // magic number // important to keep minus one for correct size
        levelMap = FillLevelWithBarriers(levelMap, 7); // magic number for test

        return levelMap;
    }

    // private Barrier[,] FillLevelWithBarriers(Barrier[,] barriers, int trapsQuantity, int trapsLevel, int obstaclesQuantity, int obstaclesLevel)
    private Barrier[,] FillLevelWithBarriers(Barrier[,] barriers, int obstaclesQuantity)
    {
        for (int i = 0; i < barriers.GetLength(0); i++)
        {
            for (int j = 0; j < barriers.GetLength(1); j++)
            {
                if (obstaclesQuantity > 0)
                {
                    barriers[i, j] = _obstacles[0];
                    obstaclesQuantity--;
                }
                else
                {
                    break;
                }
            }
        }

        return barriers;
    }
    
    private int CalculateFloorsQuantity(int levelsPassed, int floorsAmountFromPreviousLevel) 
    {
        if (levelsPassed % 2 == 0)
        {
            int increasedFloorsAmount = floorsAmountFromPreviousLevel + 1;
            return increasedFloorsAmount;
        }

        return floorsAmountFromPreviousLevel;
    }

    private int CalculateObstaclesQuantity(int currentFloorsAmount, int trapsAmount)
    {
        return (currentFloorsAmount - 1) * _difficultyFactor - trapsAmount - _obstaclesReducer; // refer to RoofsLoader 
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

    private int CalculateTrapsQuantity(int levelsPassed, int currentFloorsAmount)
    {
        if (levelsPassed >= _trapsAppearsOnLevel)
        {
            return currentFloorsAmount - 2;
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
