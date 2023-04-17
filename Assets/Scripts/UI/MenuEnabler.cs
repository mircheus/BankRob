using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuEnabler : MonoBehaviour
{
    [SerializeField] private Robbery _robbery;
    [SerializeField] private Shop _shop;
    [SerializeField] private RobStarter _robStarter;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _loosePanel;
    [SerializeField] private GameObject _prepareMenu;
    [SerializeField] private GameObject _warningPanel;
    [SerializeField] private GameObject _notEnoughMoneyPanel;
    [SerializeField] private GameObject _allSlotsBusyPanel;

    private void OnEnable()
    {
        _robbery.BankRobbed += ShowWinPanel;
        _robStarter.NotEnoughRobbers += ShowWarningPanel;
        _shop.NotEnoughMoney += ShowNotEnoughMoneyMenu;
        _shop.AllSlotsBusy += ShowAllSlotsBusy;
    }

    private void OnDisable()
    {
        _robbery.BankRobbed -= ShowWinPanel;
        _robStarter.NotEnoughRobbers -= ShowWarningPanel;
        _shop.NotEnoughMoney -= ShowNotEnoughMoneyMenu;
        _shop.AllSlotsBusy -= ShowAllSlotsBusy;
    }

    private void Start()
    {
        ShowPrepareMenu();
    }

    private void ShowPrepareMenu()
    {
        _prepareMenu.SetActive(true);
    }

    private void ShowWinPanel()
    {
        _winPanel.SetActive(true);
    }

    private void ShowWarningPanel()
    {
        _warningPanel.SetActive(true);
    }

    private void ShowLosePanel()
    {
        _loosePanel.SetActive(true);
    }

    private void ShowNotEnoughMoneyMenu()
    {
        _notEnoughMoneyPanel.SetActive(true);
    }
    
    private void ShowAllSlotsBusy()
    {
        _allSlotsBusyPanel.SetActive(true);
    }
}
