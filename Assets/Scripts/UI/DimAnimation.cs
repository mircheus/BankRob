using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DimAnimation : MonoBehaviour
{
    [SerializeField] private float _dimValue;
    [SerializeField] private float _duration;
    
    private void OnEnable()
    {
        FadeTo(_dimValue);
    }

    private void OnDisable()
    {
        FadeTo(0);
    }

    private void FadeTo(float value)
    {
        GetComponent<Image>().DOFade(value, _duration);
    }
}
