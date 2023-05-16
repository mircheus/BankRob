using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Barrier
{
    [SerializeField] private ParticleSystem _destroyFx;
    
    private const float Seconds = .3f;
    
    public virtual void GetDestroyedBy()
    {
        gameObject.SetActive(false);
    }
    
    protected virtual void PlayDestroyFx()
    {
        _destroyFx.Play();
    }

    protected IEnumerator DeactivateGameObject()
    {
        yield return new WaitForSeconds(Seconds);
        gameObject.SetActive(false);
    }
}


