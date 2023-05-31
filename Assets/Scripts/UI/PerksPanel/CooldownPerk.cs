using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CooldownPerk : MonoBehaviour
{
    [SerializeField] private Image _cooldownImage;
    [SerializeField] private PerkActivator _perkActivator;
    
    public void ActivateCooldown()
    {
        // _cooldownImage.gameObject.SetActive(true);
        _cooldownImage.fillAmount = 1;
        _cooldownImage.DOFillAmount(0, _perkActivator.CoolDownTime);
        // _cooldownImage.gameObject.SetActive(false);
    }
}
