using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkSlot : MonoBehaviour
{
    [SerializeField] private FreezedButton _freezedButton;
    
    private PerkActivator _perkActivator;
    private int _columnIndex = -1;

    public int ColumnIndex => _columnIndex;

    private void OnEnable()
    {
        _perkActivator = GetComponentInChildren<PerkActivator>();

        if (_perkActivator != null)
        {
            _columnIndex = _perkActivator.ColumnIndex;
        }
    }

    public void SubscribeToRobber(Robber robber)
    {
        robber.Frozen += OnFrozen;
        Debug.Log("Subscribed to OnFrozen");
    }

    private void OnFrozen(int columnIndex)
    {
        _freezedButton.gameObject.transform.SetSiblingIndex(1);
        _freezedButton.gameObject.SetActive(true);
    }
}
