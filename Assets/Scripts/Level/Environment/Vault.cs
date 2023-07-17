using UnityEngine;

[RequireComponent(typeof(AudioOneShot))]
public class Vault : MonoBehaviour
{
    private Animator _animator;
    private AudioOneShot _audioOneShot;
    private int _openVault = Animator.StringToHash("OpenVault");

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
        }
    }
}
