using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuEnabler : MonoBehaviour
{
    [SerializeField] private Robbery _robbery;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _loosePanel;
    [SerializeField] private GameObject _prepareMenu;

    private void OnEnable()
    {
        _robbery.BankRobbed += ShowWinPanel;
    }

    private void OnDisable()
    {
        _robbery.BankRobbed -= ShowWinPanel;
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

    private void ShowLosePanel()
    {
        _loosePanel.SetActive(true);
    }
}
