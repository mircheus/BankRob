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
        _floorsQuantity = CalculateFloorsQuantity(_playerData.CompletedLevelsCounter);
    }
    
    private int CalculateFloorsQuantity(int levelsPassed)
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
}
