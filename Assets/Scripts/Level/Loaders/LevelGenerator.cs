using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const int ColumnsQuantity = 4;
    
    [SerializeField] private Progression _progression;
    [SerializeField] private RoofsLoader _roofsLoader;
    [SerializeField] private ObstacleLoader _obstacleLoader;
    [SerializeField] private TrapsLoader _trapsLoader;
    [SerializeField] private KeyLoader _keyLoader;
    [SerializeField] private BarriersLoader _barriersLoader;
    
    // [Header("DEBUG")]

    // [Header("TESTING")] 
    // [SerializeField] private PlayerData _playerData;

    // private bool[,] _obstaclesGrid; // может через неё сделать 
    // private Vector3[] _obstaclesPositions;
    // private List<Obstacle> _obstacles;
    private int _floorsQuantity;
    // private int _obstaclesQuantity;
    // private int _obstaclesLevel;
    // private int _trapsQuantity;
    // private int _trapsLevel;
    //
    // public int FloorsQuantity => _floorsQuantity;
    // public int ObstaclesLevel => _obstaclesLevel;
    // public int ObstaclesQuantity => _obstaclesQuantity;
    // public bool[,] ObstaclesGrid => _obstaclesGrid;
    // public List<Obstacle> Obstacles => _obstacles;

    private void OnEnable()
    {
        // _progression.LevelParametersPrepared += OnLevelParametersPrepared;
        _progression.LevelMapPrepared += OnLevelMapPrepared;
    }

    private void OnDisable()
    {
        // _progression.LevelParametersPrepared -= OnLevelParametersPrepared;
        _progression.LevelMapPrepared += OnLevelMapPrepared;
    }

    private void OnLevelParametersPrepared()
    {
        // for DataReflector.cs
        _floorsQuantity = _progression.FloorsQuantity;
        // _obstaclesQuantity = _progression.ObstaclesQuantity;
        // _obstaclesQuantity = _progression.ObstaclesQuantity;
        // _obstaclesLevel = _progression.ObstaclesLevel;
        _roofsLoader.ArrangeObjects(_progression.FloorsQuantity);
        _roofsLoader.GenerateFloor();
        _obstacleLoader.SetObstaclesAmount(_progression.ObstaclesQuantity);
        _obstacleLoader.SetObstaclesLevel(_progression.ObstaclesLevel);
        _trapsLoader.SetTrapsAmount(_progression.TrapsQuantity);
        _trapsLoader.SetTrapsLevel(_progression.TrapsLevel);
        
        _obstacleLoader.ArrangeObjects(_progression.FloorsQuantity);
        List<Vector3> availableCells = _obstacleLoader.AvailableCells;
        _trapsLoader.ArrangeTraps(availableCells);
        // KEYS TEMPORARILY DISABLED
        // _keyLoader.SetKeysQuantity(_progression.KeysQuantity);
        // _keyLoader.ArrangeKeys(keyPositions);
    }

    private void OnLevelMapPrepared(Barrier[,] levelMap)
    {
        PrepareRoofsWithGround();
        _barriersLoader.ArrangeBarriers(levelMap);
    }

    private void PrepareRoofsWithGround()
    {
        _floorsQuantity = _progression.FloorsQuantity;
        _roofsLoader.ArrangeObjects(_progression.FloorsQuantity);
        _roofsLoader.GenerateFloor();
    }
}