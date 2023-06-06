using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fridge : Trap
{
    [Header("Unique")]
    [SerializeField] private ParticleSystem _frozeFx;
    [SerializeField] private GameObject _fridgeModel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Robber robber))
        {
            _frozeFx.Play();
            robber.GetFrozen();
            _fridgeModel.SetActive(false);
            StartCoroutine(DeactivateGameObject());
        }
    }

    public override void GetFreezedBy(FreezePerk freezePerk)
    {
    }

    protected override IEnumerator DeactivateGameObject()
    {
        yield return new WaitForSeconds(Seconds);
        gameObject.SetActive(false);
    }
}
