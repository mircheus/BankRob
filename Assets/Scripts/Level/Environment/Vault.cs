using System;
using UnityEngine;

[RequireComponent(typeof(AudioOneShot))]
public class Vault : MonoBehaviour
{
    [SerializeField] private Transform _downTarget;
    [SerializeField] private Transform _runAwayTarget;
    [SerializeField] private Transform _runAwayTarget2;
    [SerializeField] private Transform[] _runAwayTargets;
    private Animator _animator;
    private AudioOneShot _audioOneShot;
    private int _openVault = Animator.StringToHash("OpenVault");

    public Transform DownTarget => _downTarget;
    // public Transform RunAwayTarget => _runAwayTarget;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioOneShot = GetComponent<AudioOneShot>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Robber robber))
        {
            _animator.Play(_openVault);
            _audioOneShot.PlayOneShot();
            robber.ReachedVault?.Invoke(robber);
            robber.RobberMovement.SetRunAwayTargets(_runAwayTargets);
            // robber.RobberMovement.SetTarget(transform.position + Vector3.forward * 100); // TODO : remove magic number
            // robber.RobberMovement.RunAwayFromScene(_runAwayTarget);
        }
    }

    public void PlayOpenAnimation()
    {
        _animator.Play(_openVault);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + Vector3.forward * 100, 3); //  TODO: remove magic numbers
    }
}
