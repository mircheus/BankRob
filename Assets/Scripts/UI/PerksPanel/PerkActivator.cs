using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PerkActivator : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private float _coolDownTime;
    
    private int _columnIndex;

    public int ColumnIndex => _columnIndex;
    public float CoolDownTime => _coolDownTime;

    public event UnityAction<int> PerkActivated;

    public void SetColumnIndex(int index)
    {
        _columnIndex = index;
    }
    
    public void OnClick()
    {
        PerkActivated?.Invoke(_columnIndex);
        StartCoroutine(CooldownButton());
    }

    private IEnumerator CooldownButton()
    {
        _button.interactable = false;
        yield return new WaitForSeconds(_coolDownTime);
        _button.interactable = true;
    }
}
