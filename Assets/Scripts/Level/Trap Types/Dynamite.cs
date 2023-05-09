using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : Trap
{
    [SerializeField] private ParticleSystem _explosionFx;
    [SerializeField] private ParticleSystem _fuseFx;
    [SerializeField] private GameObject _dynamiteModel;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Robber robber))
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        _explosionFx.Play();
        _fuseFx.gameObject.SetActive(false);
        _dynamiteModel.SetActive(false);
    }
}
