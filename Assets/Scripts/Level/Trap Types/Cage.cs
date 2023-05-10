using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : Trap
{
    [SerializeField] private ParticleSystem _crackFx;
    [SerializeField] private Animator _animator;

    private int _close = Animator.StringToHash("Close");
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RobberMovement robberMovement))
        {
            _animator.Play(_close);
            Debug.Log("Caged");
            robberMovement.GetTrapped();
        }
    }
    
    public override void GetDestroyedByPerk()
    {
        StartCoroutine(DeactivateGameObject());
        
    }
    
    private void PlayTrappedFx()
    {
        _crackFx.Play();
    }
}
