using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    private const int ColumnsQuantity = 4;
    
    [SerializeField] private protected GameObject _obstaclePrefab; // переименовать 
    private protected int _floorQuantity;
    private float _horizontalStep = -7.5f;
    private protected int _verticalStep = -9;
    private bool[,] _buildingGrid;
    // private List<Vector3> _obstaclePositions = new List<Vector3>();
    // private List<GameObject> _obstacles = new List<GameObject>();

    // public List<GameObject> Obstacles => _obstacles;

    // [Header("DEBUG")]
    // [SerializeField] private List<Vector3> _potentialKeyPositions;

    // public List<Vector3> PotentialKeyPositions => _potentialKeyPositions;
    // public bool[,] BuildingGrid => _buildingGrid;
    
    public void ArrangeObjects(int floorQuantity)
    {
        SetFloorQuantity(floorQuantity);
        Vector3 position = transform.position;
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

    protected virtual void SetCursorIn(Vector3 position, Transform parent) // WORKAROUND METHOD NAME
    {
        TryGenerateObjectInPosition(position, parent);
    }

    protected virtual void SetFloorQuantity(int floorQuantity)
    {
        _floorQuantity = floorQuantity;
    }

    protected virtual bool TryGenerateObjectInPosition(Vector3 position, Transform parent)
    {
        // _obstacles.Add(Instantiate(_obstaclePrefab, position, Quaternion.identity, parent));
        Instantiate(_obstaclePrefab, position, Quaternion.identity, parent);
        return true;
    }
    
    // protected virtual void GenerateObjectInPosition(Vector3 position, Transform parent)
    // {
    //     Instantiate(_obstacle, position, Quaternion.identity, parent);
    //     // return true;
    // }
}
