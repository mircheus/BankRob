using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CooldownPerk : MonoBehaviour
{
    [SerializeField] private Image _cooldownImage;
    [SerializeField] private PerkActivator _perkActivator;
    
    public void ActivateCooldown()
    {
        _cooldownImage.fillAmount = 1;
        _cooldownImage.DOFillAmount(0, _perkActivator.CoolDownTime);
    }
}
