using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioOneShot))]
public class Cage : Trap
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject[] _forms;

    private int _close = Animator.StringToHash("Close");
    private AudioOneShot _audioOneShot;
    
    private void Start()
    {
        _audioOneShot = GetComponent<AudioOneShot>();
    }

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
        _audioOneShot.PlayOneShot();
        _forms[0].SetActive(false);
        _forms[1].SetActive(true);
    }
}
