using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ContinueButtonEnabler : MonoBehaviour
{
    [SerializeField] private GameObject _continueButton;
    [SerializeField] private SavegameChecker _savegameChecker;

    private void Start()
    {
        if (_savegameChecker.IsAnySavegamePersist())
        {
            EnableContinueButton();
        }
    }

    private void EnableContinueButton()
    {
        _continueButton.gameObject.SetActive(true);
    }
}   
