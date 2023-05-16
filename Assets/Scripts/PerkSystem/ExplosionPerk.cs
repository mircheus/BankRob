using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ExplosionPerk : Perk
{
    private const float DeactivationTime = .7f;
    
    [SerializeField] private int _explosionDamage;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Obstacle obstacle))
        {
            obstacle.ApplyDamage(_explosionDamage);
        }

        if (other.TryGetComponent(out Roof roof))
        {
            roof.DestroyRoof();
        }

        if (other.TryGetComponent(out Dynamite dynamite))
        {
            dynamite.GetDestroyedBy();
        }

        if (other.TryGetComponent(out Cage cage))
        {
            cage.GetDestroyedBy();
        }
    }
    

    protected override void Deactivate()
    {
        base.Deactivate();
        Debug.Log("ExplosionPerk Deactivated");
    }
}
