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

    private int _damageFormsAmount;
    private int _currentDamage = 0;
    
    public event UnityAction Destroyed;

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
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        // SwitchToDamagedForm();
        NextDamageForm();
        
        if (_health <= 0)
        {
            Destroyed?.Invoke();
        }
    }

    private void OnDestroyed()
    {
        gameObject.SetActive(false);
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

    // private void SwitchToDamagedForm()
    // {
    //     // _undamagedForm.SetActive(false);
    //     _damagedForm.SetActive(true);
    // }

    private void NextDamageForm()
    {
        if (_currentDamage + 1 < _damageFormsAmount)
        {
            _damagedForms[_currentDamage].SetActive(false);
            _currentDamage++;
            _damagedForms[_currentDamage].SetActive(true);
        }
    }
}
