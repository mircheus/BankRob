using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofsLoader : Loader
{
    [SerializeField] private Ground _ground;
    
    private float _groundOffset;

    // protected override void Start()
    // {
    //     base.Start();
    // }

    public void GenerateFloor()
    {
        CalculateFloorOffset();
        SetFloor();
    }
    
    private void CalculateFloorOffset()
    {
        _groundOffset = _verticalStep * (_floorQuantity - 1);
    }

    private void SetFloor()
    {
        Vector3 offset = new Vector3(0, _groundOffset, 0);
        _ground.transform.position = offset;
    }
}
