using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int _health;
    // [SerializeField] private GameObject _undamagedForm;
    // [SerializeField] private GameObject _damagedForm;
    [SerializeField] private GameObject[] _damagedForms;
    [SerializeField] private ParticleSystem _damageFX;
    [SerializeField] private ParticleSystem _destroyFX;
    [SerializeField] private float _destroyDelay;

    private int _damageFormsAmount;
    private int _currentDamage = 0;
    private BoxCollider _boxCollider;
    
    public event UnityAction<Vector3> Destroyed;

    private void OnEnable()
    {
        Destroyed += OnDestroyed;
    }

    private void OnDisable()
    {
        Destroyed -= OnDestroyed;
    }

    private void Start()
    {
        _damageFormsAmount = _damagedForms.Length;
        _currentDamage = 0;
        InitializeUndamagedForm();
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        NextDamageForm();
        _damageFX.Play();
        
        if (_health <= 0)
        {
            Destroyed?.Invoke(transform.position);
        }
    }

    private void OnDestroyed(Vector3 position)
    {
        // gameObject.SetActive(false);
        _destroyFX.Play();
        _boxCollider.isTrigger = true;
        _damagedForms[_currentDamage].SetActive(false);
        StartCoroutine(DisableAfterSomeTime());
    }

    private void InitializeUndamagedForm()
    {
        // _undamagedForm.SetActive(true);
        _damagedForms[0].SetActive(true);
        Debug.Log("point");

        for (int i = 1; i < _damagedForms.Length; i++)
        {
            _damagedForms[i].SetActive(false);
        }

        // _damagedForm.SetActive(false); // для одной damage формы
    }

    private void NextDamageForm()
    {
        if (_currentDamage + 1 < _damageFormsAmount)
        {
            _damagedForms[_currentDamage].SetActive(false);
            _currentDamage++;
            _damagedForms[_currentDamage].SetActive(true);
        }
    }

    private IEnumerator DisableAfterSomeTime()
    {
        yield return new WaitForSeconds(_destroyDelay);
        Debug.Log("Waited destroy delay");
        gameObject.SetActive(false);
    }
}
