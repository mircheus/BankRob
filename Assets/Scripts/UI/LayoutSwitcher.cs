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
    [SerializeField] private AspectRatioChecker _aspectRatioChecker;

    private void OnEnable()
    {
        _aspectRatioChecker.AspectRatioChanged += OnAspectRatioChanged;
        SetInitialLayout();
    }

    private void OnDisable()
    {
        _aspectRatioChecker.AspectRatioChanged -= OnAspectRatioChanged;
    }

    private void OnAspectRatioChanged()
    {
        if (_aspectRatioChecker.IsMobileScreen)
        {
            SwitchToHorizontalLayout();
        }

        if (_aspectRatioChecker.IsMobileScreen == false)
        {
            SwitchToVerticalLayout();
        }
    }

    private void SetInitialLayout()
    {
        if (_aspectRatioChecker.AspectRatio < 1)
        {
            SwitchToVerticalLayout();
        }
        else
        {
            SwitchToHorizontalLayout();
        }
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
