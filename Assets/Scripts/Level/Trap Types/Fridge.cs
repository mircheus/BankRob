using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fridge : Trap
{
    [Header("Unique")]
    [SerializeField] private ParticleSystem _frozeFx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Robber robber))
        {
            Debug.Log("Get Fridged");
            _frozeFx.Play();
            robber.GetFrozen();
        }
    }
}
