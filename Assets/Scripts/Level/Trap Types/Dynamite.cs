using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioOneShot))]
public class Dynamite : Trap
{
    [SerializeField] private ParticleSystem _fuseFx;
    [SerializeField] private GameObject _dynamiteModel;

    private AudioOneShot _audioOneShot;

    private void Start()
    {
        _audioOneShot = GetComponent<AudioOneShot>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Robber robber))
        {
            if (_isTrapActive)
            {
                PlayDestroyFx();
                PlayExplosionSound();
                robber.RobberMovement.GetTrappedBy(this);
            }
        }
    }   
    
    public override void GetDestroyed()
    {
        PlayDestroyFx();
        PlayExplosionSound();
        StartCoroutine(DeactivateGameObject());
    }

    public override void GetFreezedBy(FreezePerk freezePerk)
    {
        base.GetFreezedBy(freezePerk);
        _fuseFx.Stop();
        _isTrapActive = false;
    }
    
    protected override void PlayDestroyFx()
    {
        if (_isTrapActive)
        {
            base.PlayDestroyFx();
        }
        
        _fuseFx.gameObject.SetActive(false);
        _dynamiteModel.SetActive(false);
    }

    private void PlayExplosionSound()
    {
        _audioOneShot.PlayOneShot();
    }
}
