using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofsLoader : MonoBehaviour
{
    private const int ColumnsQuantity = 4;
    
    [SerializeField] private Roof _roofPrefab;
    [SerializeField] private Ground _ground;
    
    private int _floorsQuantity;
    private float _horizontalStep = 7.5f;
    private int _verticalStep = -9;
    private float _currentOffsetY;

    private void Awake()
    {
        _floorsQuantity = GetComponentInParent<Generator>().FloorsQuantity;
    }

    private void Start()
    {
        CreateObstacles(_floorsQuantity);
        SetFloor(_currentOffsetY);
    }

    private void CreateObstacles(int floorQuantity)
    {
        Vector3 position = transform.position;
        Vector3 horizontalOffset = new Vector3(_horizontalStep, 0, 0);
        Vector3 verticalOffset = new Vector3(0, _verticalStep, 0);
        Vector3 currentOffset = new Vector3();
        Transform parent = transform;

        for (int i = 0; i < floorQuantity; i++)
        {
            for (int j = 0; j < ColumnsQuantity; j++)
            {
                currentOffset = parent.position + horizontalOffset * j + verticalOffset * i;
                Instantiate(_roofPrefab, currentOffset, Quaternion.identity, parent);
            }
        }

        _currentOffsetY = currentOffset.y;
    }

    private void SetFloor(float floorOffset)
    {
        Vector3 offset = new Vector3(0, floorOffset + _verticalStep, 0);
        _ground.transform.position = offset;
    }
}
