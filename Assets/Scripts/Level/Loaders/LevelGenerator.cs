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
    [SerializeField] private KeyLoader _keyLoader;
    
    [Header("DEBUG")]
    [SerializeField] private int _floorsQuantity;

    [Header("TESTING")] 
    [SerializeField] private PlayerData _playerData;

    private bool[,] _obstaclesGrid; // может через неё сделать 
    private Vector3[] _obstaclesPositions;
    private List<Obstacle> _obstacles;

    public int FloorsQuantity => _floorsQuantity;
    public bool[,] ObstaclesGrid => _obstaclesGrid;
    public List<Obstacle> Obstacles => _obstacles;

    private void OnEnable()
    {
        // _playerData.DataLoaded += OnDataLoaded;
        _progression.LevelParametersPrepared += OnLevelParametersPrepared;
    }

    private void OnDisable()
    {
        // _playerData.DataLoaded -= OnDataLoaded;
        _progression.LevelParametersPrepared -= OnLevelParametersPrepared;
    }

    private void OnLevelParametersPrepared()
    {
        _floorsQuantity = _progression.FloorsQuantity;
        Debug.Log(_floorsQuantity);
        _obstaclesGrid = new bool[_floorsQuantity, ColumnsQuantity];
        _roofsLoader.ArrangeObjects(_floorsQuantity);
        _roofsLoader.GenerateFloor();
        _obstacleLoader.ArrangeObjects(_floorsQuantity);
        List<Vector3> keyPositions = _obstacleLoader.PossibleKeyPositions;
        _keyLoader.ArrangeKeys(keyPositions);
    }
    
    // private void SetLevelParameters(int floorsQuantity, int )
}
