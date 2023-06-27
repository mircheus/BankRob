using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonDisabler : AdButtonDisabler
{
    [SerializeField] private Image _robberIcon;

    public override void MakeNotInteractable()
    {
        base.MakeNotInteractable();
        var image = _robberIcon;
        var tempColor = image.color;
        tempColor.a = HalfTransparent;
        image.color = tempColor;
        
    }
}
