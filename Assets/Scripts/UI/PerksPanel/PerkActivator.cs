using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PerkActivator : MonoBehaviour
{
    // [SerializeField] private GameObject _perk;
    [SerializeField] private Button _button;
    [SerializeField] private float _coolDownTime;

    public float CoolDownTime => _coolDownTime;

    private int _columnIndex;
    private Robber _robber;

    public event UnityAction<int> PerkActivated;

    public int ColumnIndex => _columnIndex;

    public void SetColumnIndex(int index)
    {
        _columnIndex = index;
    }

    public void SetRobber(Robber robber)
    {
        _robber = robber;
    }
    
    public void OnClick()
    {
        PerkActivated?.Invoke(_columnIndex);
        StartCoroutine(CooldownButton());
        Debug.Log("PerkActivated");
    }

    private IEnumerator CooldownButton()
    {
        _button.interactable = false;
        yield return new WaitForSeconds(_coolDownTime);
        _button.interactable = true;
    }
}
