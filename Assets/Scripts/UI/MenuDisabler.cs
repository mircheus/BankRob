using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuDisabler : MonoBehaviour
{
    [SerializeField] private GameStarter _gameStarter;
    [SerializeField] private GameObject _menu;

    private void OnEnable()
    {
        _gameStarter.RobStarted += DisableMenu;
    }

    private void OnDisable()
    {
        _gameStarter.RobStarted -= DisableMenu;
    }

    private void DisableMenu()
    {
        _menu.SetActive(false);
    }
}
