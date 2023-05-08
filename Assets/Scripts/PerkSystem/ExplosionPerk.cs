using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.WSA;

public class ExplosionPerk : Perk
{
    [SerializeField] private int _explosionDamage;
    
    private void OnEnable()
    {
        StartCoroutine(DeactivateAfterExecution());
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

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
    }

    private IEnumerator DeactivateAfterExecution()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
