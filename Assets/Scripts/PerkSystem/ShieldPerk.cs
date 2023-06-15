using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPerk : Perk
{
    [SerializeField] private Robber _thisRobber;

    private void OnEnable()
    {
        _thisRobber.Shield.Activate();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Robber robber))
        {
            robber.Shield.Activate();
        }
    }
}
