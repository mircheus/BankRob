using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Barrier
{
    [SerializeField] private ParticleSystem _destroyFx;
    [SerializeField] private Obstacle _iceCube;
    
    private const float Seconds = .3f;
    
    public virtual void GetDestroyedBy()
    {
        gameObject.SetActive(false);
    }

    public virtual void GetFreezedBy(FreezePerk freezePerk)
    {
        _iceCube.gameObject.SetActive(true);
        _iceCube.Destroyed += GetDestroyedBy;
    }
    
    protected virtual void PlayDestroyFx()
    {
        _destroyFx.Play();
    }

    protected IEnumerator DeactivateGameObject()
    {
        yield return new WaitForSeconds(Seconds);
        _iceCube.Destroyed -= GetDestroyedBy;
        gameObject.SetActive(false);
    }
}


