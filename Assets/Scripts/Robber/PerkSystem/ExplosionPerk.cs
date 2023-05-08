using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExplosionPerk : MonoBehaviour
{
    [SerializeField] private int _explosionDamage;

    private void OnEnable()
    {
        StartCoroutine(DeactivateAfterExecution());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Obstacle obstacle))
        {
            Debug.Log("COllided with obstacle");
            obstacle.ApplyDamage(_explosionDamage);
        }
    }

    private IEnumerator DeactivateAfterExecution()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
