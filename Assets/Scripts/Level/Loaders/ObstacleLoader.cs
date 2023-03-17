using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleLoader : MonoBehaviour
{
    private const int ColumnsQuantity = 4;

    [SerializeField] private GameObject[] _obstacles; // переделать на массив Obstacles
    
    private int _floorsQuantity;
    private float _horizontalStep = 7.5f;
    private int _verticalStep = -9;
    private ObstacleLoaderChild _child;

    private void Awake()
    {
        _floorsQuantity = GetComponentInParent<Generator>().FloorsQuantity - 1;
    }

    private void Start()
    {
        CreateObstacles(_floorsQuantity);
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
                
                if (Random.Range(0, 2) == 1)
                {
                    int randomIndex = Random.Range(0, _obstacles.Length);
                    Instantiate(_obstacles[randomIndex], currentOffset, Quaternion.identity, parent);
                }
            }
        }
    }
}
