using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dynamite : Trap
{
    [SerializeField] private ParticleSystem _fuseFx;
    [SerializeField] private GameObject _dynamiteModel;

    // private bool _isFreezed = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Robber robber))
        {
            if (_isTrapActive)
            {
                PlayDestroyFx();
                robber.GetComponent<RobberMovement>().GetTrappedBy(this);
            }
        }
    }   
    
    public override void GetDestroyed()
    {
        PlayDestroyFx();
        StartCoroutine(DeactivateGameObject());
    }

    public override void GetFreezedBy(FreezePerk freezePerk)
    {
        base.GetFreezedBy(freezePerk);
        _fuseFx.Stop();
        // _isFreezed = true;
        _isTrapActive = false;
    }
    
    protected override void PlayDestroyFx()
    {
        // if (_isFreezed == false)
        if (_isTrapActive)
        {
            base.PlayDestroyFx();
        }
        
        _fuseFx.gameObject.SetActive(false);
        _dynamiteModel.SetActive(false);
    }
}
