using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : Trap
{
    [SerializeField] private ParticleSystem _crackFx;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject[] _forms;

    private int _close = Animator.StringToHash("Close");
    // private bool _isTrapActive = true;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RobberMovement robberMovement))
        {
            if (robberMovement.IsDashActive == false && _isTrapActive)
            {
                _animator.Play(_close);
                robberMovement.GetTrappedBy(this);
            }
            else
            {
                GetDestroyed();
            }
        }
    }
    
    public override void GetDestroyed()
    {
        _isTrapActive = false;
        PlayDestroyFx();
        _forms[0].SetActive(false);
        _forms[1].SetActive(true);
        Debug.Log("Cage Destroyed");
    }
}
