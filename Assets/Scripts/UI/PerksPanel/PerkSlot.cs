using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkSlot : MonoBehaviour
{
    [SerializeField] private PerkBlocker _freezedButton;
    [SerializeField] private PerkBlocker _deadImage;
    
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
        robber.GetComponent<RobberMovement>().GetStopped += OnGetStopped;
    }

    private void OnFrozen(int columnIndex)
    {
        _freezedButton.gameObject.transform.SetAsLastSibling();
        _freezedButton.gameObject.SetActive(true);
    }

    private void OnGetStopped()
    {
        SetActiveToTop(_deadImage);
    }

    private void SetActiveToTop(PerkBlocker perkBlocker)
    {
        perkBlocker.gameObject.transform.SetAsLastSibling();
        perkBlocker.gameObject.SetActive(true);
    }
}
