using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseMenu : MonoBehaviour
{
    [SerializeField] private RectTransform _lossPanel;
    [SerializeField] private RectTransform _lossTitle;
    [SerializeField] private Image _dimed;
    [SerializeField] private RectTransform _nextButtonDesktop;
    [SerializeField] private RectTransform _nextButtonMobile;
    [SerializeField] private RectTransform _adButtonDesktop;
    [SerializeField] private RectTransform _adButtonMobile;
    [SerializeField] private LayoutSwitcher _layoutSwitcher;

    public RectTransform LossPanel => _lossPanel;
    public RectTransform LossTitle => _lossTitle;
    public Image Dimed => _dimed;
    public bool IsMobileScreen => _layoutSwitcher.IsMobileScreen;
    public RectTransform NextButtonDesktop => _nextButtonDesktop;
    public RectTransform NextButtonMobile => _nextButtonMobile;
    public RectTransform ADButtonDesktop => _adButtonDesktop;
    public RectTransform ADButtonMobile => _adButtonMobile;
}
