using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioChecker : MonoBehaviour
{
    private bool _isMobileScreen;

    public bool IsMobileScreen => _isMobileScreen;
    public float AspectRatio => GetAspectRatio();
    
    public event Action AspectRatioChanged;

    private void Start()
    {
        SetInitialLayout();
    }

    private void Update()
    {
        float aspectRatio = GetAspectRatio(); 

        if (_isMobileScreen)
        {
            if (IsMoreThanOne(aspectRatio))
            {
                AspectRatioChanged?.Invoke();
                _isMobileScreen = false;
            }
        }

        if (_isMobileScreen == false)
        {
            if (IsLessThanOne(aspectRatio))
            {
                AspectRatioChanged?.Invoke();
                _isMobileScreen = true;
            }
        }
    }
    
    public float GetAspectRatio()
    {
        return (float)Screen.width / (float)Screen.height;
    }

    private void SetInitialLayout()
    {
        float currentAspectRatio = GetAspectRatio();
        
        if (currentAspectRatio <= 1)
        {
            _isMobileScreen = true;
        }
        else
        {
            _isMobileScreen = false;
        }
    }

    private bool IsMoreThanOne(float value)
    {
        return value >= 1;
    }

    private bool IsLessThanOne(float value)
    {
        return value <= 1;
    }
}