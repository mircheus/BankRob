using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleLoaderChild : Loader
{
    protected override void GetFloorQuantity()
    {
        base.GetFloorQuantity();

        _floorQuantity--;
    }

    protected override void GenerateObjectInPosition(Vector3 position, GameObject gameObject, Transform parent)
    {
        if (Random.Range(0, 2) == 1)
        {
            Instantiate(gameObject, position, Quaternion.identity, parent);
        }
    }
}
