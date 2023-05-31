using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FlashingButton : MonoBehaviour
{
    [SerializeField] private Image _flashingImage;
    [SerializeField] private float _duration;

    private void OnEnable()
    {
        _flashingImage.enabled = true;
        StartFlash();
    }

    public void DisableSelf()
    {
        _flashingImage.enabled = false;
    }

    private void StartFlash()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_flashingImage.DOFade(1, _duration))
            .Append(_flashingImage.DOFade(0, _duration))
            .SetLoops(-1, LoopType.Yoyo);
    }
}
