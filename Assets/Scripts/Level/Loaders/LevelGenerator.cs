using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // private const int ColumnsQuantity = 4;
    
    [SerializeField] private BarriersProgression barriersProgression;
    [SerializeField] private RoofsLoader _roofsLoader;
    [SerializeField] private BarriersLoader _barriersLoader;

    private void OnEnable()
    {
        barriersProgression.LevelMapPrepared += OnLevelMapPrepared;
    }

    private void OnDisable()
    {
        barriersProgression.LevelMapPrepared += OnLevelMapPrepared;
    }

    private void OnLevelMapPrepared(Barrier[,] levelMap)
    {
        PrepareRoofsWithGround();
        _barriersLoader.ArrangeBarriers(levelMap);
    }
    
    private void PrepareRoofsWithGround()
    {
        _roofsLoader.ArrangeObjects(barriersProgression.FloorsQuantity);
        _roofsLoader.GenerateFloor();
    }
}