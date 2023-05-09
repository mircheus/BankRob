using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsLoader : Loader
{
    [SerializeField] private protected Trap _trapPrefab; // Replace "GameObject" with class Trap
    [SerializeField] private int _trapsAmount;

    public void ArrangeTraps(List<Vector3> positions)
    {
        Transform parent = transform;

        foreach (var position in positions)
        {
            TryGenerateObjectInPosition(position, parent);
        }
    }

    protected override bool TryGenerateObjectInPosition(Vector3 position, Transform parent)
    {
        Instantiate(_trapPrefab, position, Quaternion.identity, parent);
        return true;
    }
}
