using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    private const int ColumnsQuantity = 4;
    
    [SerializeField] private protected GameObject _obstacle; // переименовать 
    private protected int _floorQuantity;
    private float _horizontalStep = 7.5f;
    private protected int _verticalStep = -9;

    private void Awake()
    {
        GetFloorQuantity();
    }

    protected virtual void Start()
    {
        ArrangeObjects(_floorQuantity);
    }

    private void ArrangeObjects(int floorQuantity)
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
                GenerateObjectInPosition(currentOffset, parent);
            }
        }
    }

    protected virtual void GetFloorQuantity()
    {
        _floorQuantity = GetComponentInParent<Generator>().FloorsQuantity;
    }

    protected virtual void GenerateObjectInPosition(Vector3 position, Transform parent)
    {
        Instantiate(_obstacle, position, Quaternion.identity, parent);
    }
}
