using UnityEngine;

[RequireComponent(typeof(AudioOneShot))]
public class Vault : MonoBehaviour
{
    private Animator _animator;
    private int _openVault = Animator.StringToHash("OpenVault");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Robber robber))
        {
            _animator.Play(_openVault);
            GetComponent<AudioOneShot>().PlayOneShot();
            robber.ReachedVault?.Invoke(robber);
        }
    }
}
