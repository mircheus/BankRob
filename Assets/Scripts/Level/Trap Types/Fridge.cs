using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioOneShot))]
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
            GetComponent<AudioOneShot>().PlayOneShot();
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
