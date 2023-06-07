using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;

public class AdButtonDisabler : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _adIcon;
    [SerializeField] private TMP_Text _adText;
    
    private const float HalfTransparent = 0.5f;
    
    public void DisableButton()
    {
        _button.interactable = false;
        var image = _adIcon;
        var tempColor = image.color;
        tempColor.a = HalfTransparent;
        image.color = tempColor;
        var text = _adText;
        var textColor = text.color;
        textColor.a = HalfTransparent;
        text.color = textColor;
        _button.GetComponent<RectTransform>().localScale = Vector3.one;
        MenuAnimator.KillAllTweens();
    }
}
