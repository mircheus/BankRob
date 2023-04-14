using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuDisabler : MonoBehaviour
{
    [SerializeField] private RobStarter _robStarter;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _warningPanel;
    [SerializeField] private GameObject _notEnoughMoneyPanel;
    [SerializeField] private GameObject _allSlotsBusyPanel;
    [SerializeField] private GameObject _robbersGrid;
    
    private void OnEnable()
    {
        _robStarter.Started += DisablePreparingMenu;
    }

    private void OnDisable()
    {
        _robStarter.Started -= DisablePreparingMenu;
    }

    private void DisablePreparingMenu()
    {
        _menu.SetActive(false);
        _robbersGrid.SetActive(false);
    }

    public void DisableWarningMenu()
    {
        _warningPanel.SetActive(false);
    }

    public void DisableNotEnoughMoneyMenu()
    {
        _notEnoughMoneyPanel.SetActive(false);
    }
    
    public void DisableAllSlotsBusy()
    {
        _allSlotsBusyPanel.SetActive(false);
    }
}
