using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLoader : MonoBehaviour
{
    [SerializeField] private Obstacle _obstaclePrefab;
    [SerializeField] private int _floorsQuantity;
    
    private const int ColumnsQuantity = 4;
    private float _horizontalStep = 7.5f;
    private int _verticalStep = -9;

    private void Start()
    {
        CreateObstacles(_floorsQuantity);
    }

    private void CreateObstacles(int floorQuantity)
    {
        Vector3 position = transform.position;
        Vector3 horizontalOffset = new Vector3(7.5f, 0, 0);
        Vector3 verticalOffset = new Vector3(0, -9, 0);
        Vector3 currentOffset;
        Transform parent = transform;

        for (int i = 0; i < floorQuantity; i++)
        {
            for (int j = 0; j < ColumnsQuantity; j++)
            {
                currentOffset = parent.position + horizontalOffset * j + verticalOffset * i;
                Instantiate(_obstaclePrefab, currentOffset, Quaternion.identity, parent);
            }
        }
    }
}
