using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PerkActivator : MonoBehaviour
{
    [SerializeField] private GameObject _perk;
    [SerializeField] private Button _button;
    [SerializeField] private float _coolDownTime;
    
    public event UnityAction Activated;
    
    public void TestButton()
    {
        Activated?.Invoke();
        _perk.SetActive(true);
        StartCoroutine(CooldownButton());
    }

    private IEnumerator CooldownButton()
    {
        _button.interactable = false;
        yield return new WaitForSeconds(_coolDownTime);
        _button.interactable = true;
    }
}
