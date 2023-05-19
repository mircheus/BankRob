using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _popup;
    [SerializeField] private Image _dimmed;

    private bool _enabled = false;

    public void OnClick()
    {
        AnimatePopup();
        _enabled = !_enabled;
    }

    private void AnimatePopup()
    {
        if (_enabled == false)
        {
            _popup.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, .5f, false);
            _dimmed.DOFade(1, 0.5f);
        }
        else
        {
            _popup.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 1200), .5f, false);
            _dimmed.DOFade(0, 0.5f);
        }
    }
    

}
