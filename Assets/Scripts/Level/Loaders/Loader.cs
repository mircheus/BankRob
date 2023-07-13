using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    private const int ColumnsQuantity = 4;
    
    [SerializeField] private protected GameObject _obstaclePrefab;
    private protected int _floorQuantity;
    private protected float _horizontalStep = -7.5f;
    private protected int _verticalStep = -9;

    public void ArrangeObjects(int floorQuantity)
    {
        SetFloorQuantity(floorQuantity);
        Vector3 horizontalOffset = new Vector3(_horizontalStep, 0, 0);
        Vector3 verticalOffset = new Vector3(0, _verticalStep, 0);
        Vector3 currentOffset = new Vector3();
        Transform parent = transform;
        
        for (int i = 0; i < _floorQuantity; i++)
        {
            for (int j = 0; j < ColumnsQuantity; j++)
            {
                currentOffset = parent.position + horizontalOffset * j + verticalOffset * i;
                SetCursorIn(currentOffset, parent);
            }
        }
    }

    private void SetCursorIn(Vector3 position, Transform parent)
    {
        GenerateObjectInPosition(position, parent);
    }

    private void SetFloorQuantity(int floorQuantity)
    {
        _floorQuantity = floorQuantity;
    }

    protected virtual void GenerateObjectInPosition(Vector3 position, Transform parent)
    {
        Instantiate(_obstaclePrefab, position, Quaternion.identity, parent);
    }
}
