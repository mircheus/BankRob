using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;

public class ButtonInteractDisabler : MonoBehaviour
{
    private const float HalfTransparent = 0.5f;
    
    [SerializeField] private Button _button;
    [SerializeField] private Image[] _imagesToGreyOut;
    [SerializeField] private TMP_Text _text;

    public void DisableButtonWithTweens()
    {
        MenuAnimator.KillAllTweens();
        MakeNotInteractable();
    }

    public void MakeNotInteractable()
    {
        MakeImagesGreyedOut();
        var text = _text;
        var textColor = text.color;
        textColor.a = HalfTransparent;
        text.color = textColor;
        _button.GetComponent<RectTransform>().localScale = Vector3.one;
        _button.interactable = false;
    }

    private void MakeImagesGreyedOut()
    {
        foreach (var imageToGreyOut in _imagesToGreyOut)
        {
            var image = imageToGreyOut;
            var tempColor = image.color;
            tempColor.a = HalfTransparent;
            image.color = tempColor;
        }
    }
}
