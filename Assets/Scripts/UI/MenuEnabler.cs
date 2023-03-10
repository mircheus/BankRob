using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class MenuEnabler : MonoBehaviour
{
    [SerializeField] private Robbery _robbery;
    [SerializeField] private RobStarter _robStarter;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _loosePanel;
    [SerializeField] private GameObject _prepareMenu;
    [SerializeField] private GameObject _warningPanel;

    private void OnEnable()
    {
        _robbery.BankRobbed += ShowWinPanel;
        _robStarter.NotEnoughRobbers += ShowWarningPanel;
    }

    private void OnDisable()
    {
        _robbery.BankRobbed -= ShowWinPanel;
        _robStarter.NotEnoughRobbers -= ShowWarningPanel;
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
}
