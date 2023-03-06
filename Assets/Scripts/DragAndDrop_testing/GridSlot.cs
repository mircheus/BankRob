using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GridSlot : MonoBehaviour
{
    private Robber _robber;

    private void Start()
    {
        if (_robber != null)
        {
            PlaceRobberInCellCenter(_robber);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Robber robber))
        {
            if (robber.IsDraggingNow == false)
            {
                _robber = robber;
                PlaceRobberInCellCenter(robber);
            }
        }
    }

    private void PlaceRobberInCellCenter(Robber robber)
    {
        robber.transform.SetParent(gameObject.transform);
        robber.transform.position = transform.position;
        Debug.Log("Robber placed in center");
    }
}
