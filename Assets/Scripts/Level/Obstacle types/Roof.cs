using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Roof : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destroyFx;
    [SerializeField] private float _destroyDelay;

    public event UnityAction<Vector3> Destroyed;
    
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void DestroyRoof()
    {
        MakeSelfInvisible();
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
