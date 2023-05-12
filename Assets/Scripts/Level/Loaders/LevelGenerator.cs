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
    
    [Header("DEBUG")]
    [SerializeField] private int _floorsQuantity;

    // [Header("TESTING")] 
    // [SerializeField] private PlayerData _playerData;

    // private bool[,] _obstaclesGrid; // может через неё сделать 
    // private Vector3[] _obstaclesPositions;
    // private List<Obstacle> _obstacles;
    private int _obstaclesLevel;
    private int _trapsLevel;

    public int FloorsQuantity => _floorsQuantity;
    public int ObstaclesLevel => _obstaclesLevel;
    // public bool[,] ObstaclesGrid => _obstaclesGrid;
    // public List<Obstacle> Obstacles => _obstacles;

    private void OnEnable()
    {
        _progression.LevelParametersPrepared += OnLevelParametersPrepared;
    }

    private void OnDisable()
    {
        _progression.LevelParametersPrepared -= OnLevelParametersPrepared;
    }

    private void OnLevelParametersPrepared()
    {
        _floorsQuantity = _progression.FloorsQuantity;
        _obstaclesLevel = _progression.ObstaclesLevel;
        // _trapsLevel = _progression.
        
        _roofsLoader.ArrangeObjects(_floorsQuantity);
        _roofsLoader.GenerateFloor();
        
        // _obstaclesGrid = new bool[_floorsQuantity, ColumnsQuantity];
        _obstacleLoader.SetObstaclesLevel(_obstaclesLevel);
        _obstacleLoader.ArrangeObjects(_floorsQuantity);
        List<Vector3> availableCells = _obstacleLoader.AvailableCells;
        
        _trapsLoader.ArrangeTraps(availableCells);
        // KEYS TEMPORARILY DISABLED
        // _keyLoader.SetKeysQuantity(_progression.KeysQuantity);
        // _keyLoader.ArrangeKeys(keyPositions);
    }
}
