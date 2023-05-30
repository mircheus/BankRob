using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PerkActivatorTutorial : MonoBehaviour
{
    // [SerializeField] private GameObject _perk;
    [SerializeField] private Button _button;
    [SerializeField] private float _coolDownTime;
    [SerializeField] private PerksPanel _perksPanel;

    private Robber _robber;
    private int _columnIndex;

    // public event UnityAction<int> PerkActivated;

    // public int ColumnIndex => _columnIndex;
    //
    // public void SetColumnIndex(int index)
    // {
    //     _columnIndex = index;
    // }

    // public void SetRobber(Robber robber)
    // {
    //     _robber = robber;
    // }

    private void OnEnable()
    {
        _robber = _perksPanel.SendRobberForTutorialTo(this);
    }

    public void OnClick()
    {
        _robber.ActivatePerk();
        StartCoroutine(CooldownButton());
    }

    private IEnumerator CooldownButton()
    {
        _button.interactable = false;
        yield return new WaitForSeconds(_coolDownTime);
        _button.interactable = true;
    }
}
