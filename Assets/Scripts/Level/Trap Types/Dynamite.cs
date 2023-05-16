using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dynamite : Trap
{
    [SerializeField] private ParticleSystem _fuseFx;
    [SerializeField] private GameObject _dynamiteModel;

    private bool _isFreezed = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Robber robber))
        {
            if (_isFreezed == false)
            {
                PlayDestroyFx();
                robber.GetComponent<RobberMovement>().GetTrappedBy(this);
            }
        }
    }   
    
    public override void GetDestroyedBy()
    {
        PlayDestroyFx();
        StartCoroutine(DeactivateGameObject());
    }

    public override void GetFreezedBy(FreezePerk freezePerk)
    {
        base.GetFreezedBy(freezePerk);
        _fuseFx.Stop();
        _isFreezed = true;
    }
    
    protected override void PlayDestroyFx()
    {
        if (_isFreezed == false)
        {
            base.PlayDestroyFx();
        }
        
        _fuseFx.gameObject.SetActive(false);
        _dynamiteModel.SetActive(false);
    }
}
