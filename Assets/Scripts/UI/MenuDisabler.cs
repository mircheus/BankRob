using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuDisabler : MonoBehaviour
{
    [SerializeField] private RobStarter robStarter;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _warningPanel;
    
    private void OnEnable()
    {
        robStarter.Started += DisablePreparingMenu;
    }

    private void OnDisable()
    {
        robStarter.Started -= DisablePreparingMenu;
    }

    private void DisablePreparingMenu()
    {
        _menu.SetActive(false);
    }

    public void DisableWarningMenu()
    {
        _warningPanel.SetActive(false);
    }
}
