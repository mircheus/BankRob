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

    private void Start()
    {
        if (OffsetEnabler.GetAspectRatio() < 1)
        {
            _verticalButtons.SetActive(true);
            _horizontalButtons.SetActive(false);
        }
        else
        {
            _verticalButtons.SetActive(false);
            _horizontalButtons.SetActive(true);
        }
    }

    private void Update()
    {
        if (OffsetEnabler.GetAspectRatio() < 1)
        {
            _verticalButtons.SetActive(true);
            _horizontalButtons.SetActive(false);
        }
        else
        {
            _verticalButtons.SetActive(false);
            _horizontalButtons.SetActive(true);
        }
    }
}
