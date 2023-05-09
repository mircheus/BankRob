using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsLoader : Loader
{
    [SerializeField] private protected Trap[] _trapPrefabs; // Replace "GameObject" with class Trap
    [SerializeField] private int _trapsAmount;

    private int _trapsLevel;

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
        int randomIndex = Random.Range(0, _trapPrefabs.Length); // WORKAROUND length must be replaced with trapsLevel
        Instantiate(_trapPrefabs[randomIndex], position, Quaternion.identity, parent);
        return true;
    }

    public void SetTrapsLevel(int trapsLevel)
    {
        _trapsLevel = trapsLevel;
    }
}
