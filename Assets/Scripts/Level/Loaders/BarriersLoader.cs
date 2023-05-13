using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarriersLoader : Loader
{
    public void ArrangeBarriers(Barrier[,] levelMap)
    {
        Vector3 position = transform.position;
        Vector3 horizontalOffset = new Vector3(_horizontalStep, 0, 0);
        Vector3 verticalOffset = new Vector3(0, _verticalStep, 0);
        Vector3 currentOffset = new Vector3();
        Transform parent = transform;

        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                currentOffset = parent.position + horizontalOffset * j + verticalOffset * i;
                if (levelMap[i, j] != null)
                {
                    Instantiate(levelMap[i, j], currentOffset, Quaternion.identity, parent);
                }
            }
        }
    }
}
