using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private const int ColumnsQuantity = 4;

    [SerializeField] private int _floorsQuantity;
    [SerializeField] private ObstacleLoader _obstacleLoader;

    [SerializeField] private RoofsLoader _roofsLoader;
    [SerializeField] private KeyLoader _keyLoader;
    
    private bool[,] _obstaclesGrid; // может через неё сделать 
    
    public int FloorsQuantity => _floorsQuantity;
    public bool[,] ObstaclesGrid => _obstaclesGrid;

    private void Start()
    {
        _obstaclesGrid = new bool[_floorsQuantity, ColumnsQuantity];
        _roofsLoader.ArrangeObjects(_floorsQuantity);
        _roofsLoader.GenerateFloor();
        _obstacleLoader.ArrangeObjects(_floorsQuantity);
        List<Vector3> keyPositions = _obstacleLoader.PossibleKeyPositions;
        _keyLoader.ArrangeKeys(keyPositions);
    }
}
