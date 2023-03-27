using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleLoader : Loader
{
    [SerializeField] private protected GameObject[] _obstacles;
    
    protected override void GetFloorQuantity()
    {
        base.GetFloorQuantity();

        _floorQuantity--;
    }

    protected override void GenerateObjectInPosition(Vector3 position, Transform parent)
    {
        if (Random.Range(0, 2) == 1)
        {
            int randomIndex = Random.Range(0, _obstacles.Length);
            Instantiate(_obstacles[randomIndex], position, Quaternion.identity, parent);
        }
    }
}
