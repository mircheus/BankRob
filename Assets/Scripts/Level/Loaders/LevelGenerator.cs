using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // private const int ColumnsQuantity = 4;
    
    [SerializeField] private Progression _progression;
    [SerializeField] private RoofsLoader _roofsLoader;
    [SerializeField] private BarriersLoader _barriersLoader;

    private void OnEnable()
    {
        _progression.LevelMapPrepared += OnLevelMapPrepared;
    }

    private void OnDisable()
    {
        _progression.LevelMapPrepared += OnLevelMapPrepared;
    }

    private void OnLevelMapPrepared(Barrier[,] levelMap)
    {
        PrepareRoofsWithGround();
        _barriersLoader.ArrangeBarriers(levelMap);
    }
    
    private void PrepareRoofsWithGround()
    {
        _roofsLoader.ArrangeObjects(_progression.FloorsQuantity);
        _roofsLoader.GenerateFloor();
    }
}