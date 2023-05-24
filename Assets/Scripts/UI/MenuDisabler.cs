using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MenuDisabler : MonoBehaviour
{
    [SerializeField] private RobStarter _robStarter;
    [SerializeField] private Robbery _robbery;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _warningPanel;
    [SerializeField] private GameObject _notEnoughMoneyPanel;
    [SerializeField] private GameObject _allSlotsBusyPanel;
    [SerializeField] private GameObject _robbersGrid;
    [SerializeField] private GameObject _leaderboard;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _perkPanel;
    [SerializeField] private GameObject _newLevelPopup;
    
    private void OnEnable()
    {
        _robStarter.Started += DisablePreparingMenu;
        _robbery.BankRobbed += DisablePerkPanel;
        _robbery.BankNotRobbed += DisablePerkPanel;
    }

    private void OnDisable()
    {
        _robStarter.Started -= DisablePreparingMenu;
        _robbery.BankRobbed -= DisablePerkPanel;
        _robbery.BankNotRobbed -= DisablePerkPanel;
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

    public void DisableLeaderboard()
    {
        _leaderboard.SetActive(false);
    }

    public void DisablePerkPanel()
    {
        _perkPanel.SetActive(false);
    }

    public void DisableSettingsMenu()
    {
        _settingsMenu.SetActive(false);
    }

    public void DisableNewLevelPopup()
    {
        _newLevelPopup.SetActive(false);
    }
}
