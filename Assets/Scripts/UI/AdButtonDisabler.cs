using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;

public class AdButtonDisabler : MonoBehaviour
{
    protected const float HalfTransparent = 0.5f;
    private const float TwoSeconds = 2f;
    
    [SerializeField] private Button _button;
    // [SerializeField] private Image _adIcon;
    // [SerializeField] private Image _moneyIcon;
    [SerializeField] private Image[] _imagesToGreyOut;
    [SerializeField] private TMP_Text _adText;

    public void DisableButton()
    {
        _button.interactable = false;
        StartCoroutine(KillTweenWithDelay());
    }

    private IEnumerator KillTweenWithDelay()
    {
        yield return new WaitForSeconds(TwoSeconds);
        MenuAnimator.KillAllTweens();
        MakeNotInteractable();
    }

    public virtual void MakeNotInteractable()
    {
        // var image = _adIcon;
        // var tempColor = image.color;
        // tempColor.a = HalfTransparent;
        // image.color = tempColor;
        MakeImagesGreyedOut();
        var text = _adText;
        var textColor = text.color;
        textColor.a = HalfTransparent;
        text.color = textColor;
        _button.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    protected virtual void MakeImagesGreyedOut()
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
