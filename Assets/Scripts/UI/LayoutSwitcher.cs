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

    private void OnEnable()
    {
        if (OffsetEnabler.GetAspectRatio() < 1)
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
