using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class LayoutSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _verticalButtons;
    [SerializeField] private GameObject _horizontalButtons;

    private bool _isMobileScreen = false;

    public bool IsMobileScreen => _isMobileScreen;

    private void OnEnable()
    {
        if (OffsetEnabler.GetAspectRatio() < 1)
        {
            SwitchToVerticalLayout();
            _isMobileScreen = true;
        }
        else
        {
            SwitchToHorizontalLayout();
            _isMobileScreen = false;
        }
    }

    private void Update()
    {
        // TESTING
        // if (OffsetEnabler.GetAspectRatio() < 1)
        // {
        //     SwitchToVerticalLayout();
        //     _isMobileScreen = true;
        // }
        // else
        // {
        //     SwitchToHorizontalLayout();
        //     _isMobileScreen = false;
        // }
    }

    private void SwitchToVerticalLayout()
    {
        _verticalButtons.SetActive(true);
        _horizontalButtons.SetActive(false);
    }

    private void SwitchToHorizontalLayout()
    {
        _verticalButtons.SetActive(false);
        _horizontalButtons.SetActive(true);
    }
}
