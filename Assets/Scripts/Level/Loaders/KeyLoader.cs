using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEditor;
using UnityEngine;

public class KeyLoader : Loader
{
    [SerializeField] private protected Key _keyPrefab;
    private int _keysAmount;
    
    public void ArrangeKeys(List<Vector3> positions)
    {
        Transform parent = transform;
        
        foreach (var position in positions)
        {
            TryGenerateObjectInPosition(position, parent);
        }
    }
    
    protected override bool TryGenerateObjectInPosition(Vector3 position, Transform parent)
    {
        if (_keysAmount > 0 && Random.Range(0, 2) == 1)
        {
            Instantiate(_keyPrefab, position, Quaternion.identity, parent);
            _keysAmount--;
            return true;
        }

        return false;
    }

    public void SetKeysQuantity(int keysQuantity)
    {
        _keysAmount = keysQuantity;
    }
}
