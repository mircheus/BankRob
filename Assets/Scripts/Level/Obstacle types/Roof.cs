using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioOneShot))]
public class Roof : MonoBehaviour, IDestroyable
{
    [SerializeField] private ParticleSystem _destroyFx;
    [SerializeField] private float _destroyDelay;

    private MeshRenderer _meshRenderer;

    public MeshRenderer MeshRendererRenderer => _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void GetDestroyed()
    {
        MakeSelfInvisible();
        GetComponent<AudioOneShot>().PlayOneShot();
        StartCoroutine(DisableAfterSomeTime(_destroyDelay));
    }
    
    
    private IEnumerator DisableAfterSomeTime(float destroyDelay)
    {
        yield return new WaitForSeconds(destroyDelay);
        gameObject.SetActive(false);
    }

    private void MakeSelfInvisible()
    {
        _destroyFx.Play();
        _meshRenderer.gameObject.SetActive(false);
        GetComponent<BoxCollider>().isTrigger = true;
    }


}
