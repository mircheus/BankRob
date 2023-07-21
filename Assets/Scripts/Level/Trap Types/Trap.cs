using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Barrier, IDestroyable
{
    [SerializeField] private ParticleSystem _destroyFx;
    [SerializeField] private Obstacle _iceCube;

    protected bool _isTrapActive = true;
    protected const float Seconds = 5f;
    
    public virtual void GetDestroyed()
    {
        gameObject.SetActive(false);
    }

    public virtual void GetFreezedBy(FreezePerk freezePerk)
    {
        _iceCube.gameObject.SetActive(true);
        _iceCube.Destroyed += GetDestroyed;
    }
    
    protected virtual void PlayDestroyFx()
    {
        _destroyFx.Play();
    }

    protected virtual IEnumerator DeactivateGameObject()
    {
        yield return new WaitForSeconds(Seconds);
        _iceCube.Destroyed -= GetDestroyed;
        gameObject.SetActive(false);
    }
}


