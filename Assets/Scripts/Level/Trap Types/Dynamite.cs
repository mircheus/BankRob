using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dynamite : Trap
{
    [SerializeField] private ParticleSystem _fuseFx;
    [SerializeField] private GameObject _dynamiteModel;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Robber robber))
        {
            PlayDestroyFx();
            robber.GetComponent<RobberMovement>().GetTrappedBy(this);
        }
    }   
    
    public override void GetDestroyedByPerk()
    {
        PlayDestroyFx();
        StartCoroutine(DeactivateGameObject());
    }
    
    protected override void PlayDestroyFx()
    {
        base.PlayDestroyFx();
        _fuseFx.gameObject.SetActive(false);
        _dynamiteModel.SetActive(false);
    }
}
