using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofsLoader : Loader
{
    [SerializeField] private Ground _ground;
    [SerializeField] private Material _evenMaterial;
    [SerializeField] private Material _oddMaterial;

    private float _groundOffset;
    private int _counter;

    private void Start()
    {
        _counter = 1;
    }

    public void GenerateFloor()
    {
        CalculateFloorOffset();
        SetFloor();
    }
    
    private void CalculateFloorOffset()
    {
        _groundOffset = _verticalStep * (_floorQuantity - 1); // roofs loader
    }

    private void SetFloor()
    {
        var position = _ground.transform.position;
        Vector3 offset = new Vector3(position.x, _groundOffset, position.z);
        position = offset;
        _ground.transform.position = position;
    }

    protected override bool TryGenerateObjectInPosition(Vector3 position, Transform parent)
    {
        if ((_counter % 2) == 0)
        {
            GameObject roof = Instantiate(_obstaclePrefab, position, Quaternion.identity, parent);
            roof.GetComponentInChildren<MeshRenderer>().material = _evenMaterial;
            _counter++;
        }
        else
        {
            GameObject roof = Instantiate(_obstaclePrefab, position, Quaternion.identity, parent);
            roof.GetComponentInChildren<MeshRenderer>().material = _oddMaterial;
            _counter++;
        }
        
        return true;
    }
}
