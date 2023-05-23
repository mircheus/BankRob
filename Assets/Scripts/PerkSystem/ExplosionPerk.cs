using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ExplosionPerk : Perk
{
    private const float DeactivationTime = .7f;
    
    [SerializeField] private int _explosionDamage;
    [SerializeField] private ParticleSystem[] _explosionFx;

    private int _counter = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Obstacle obstacle))
        {
            obstacle.ApplyDamage(_explosionDamage);
            PlayFxAt(obstacle.transform.position);
        }

        if (other.TryGetComponent(out Roof roof))
        {
            roof.DestroyRoof();
            PlayFxAt(roof.transform.position);
        }

        if (other.TryGetComponent(out Dynamite dynamite))
        {
            dynamite.GetDestroyed();
            PlayFxAt(dynamite.transform.position);
        }

        if (other.TryGetComponent(out Cage cage))
        {
            cage.GetDestroyed();
            PlayFxAt(cage.transform.position);
        }
    }

    protected override void Deactivate()
    {
        base.Deactivate();
        Debug.Log("ExplosionPerk Deactivated");
    }

    private void PlayFxAt(Vector3 position)
    {
        _explosionFx[_counter].transform.position = position;
        _explosionFx[_counter].Play();
        _counter++;

        if (_counter == _explosionFx.Length)
        {
            _counter = 0;
        }
    }
}
