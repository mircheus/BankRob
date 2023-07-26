using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private RectTransform _winPanel;
    [SerializeField] private RectTransform _winTitle;
    [SerializeField] private Image _dimed;
    [SerializeField] private RectTransform _newItem;
    [SerializeField] private RectTransform _nextButtonDesktop;
    [SerializeField] private RectTransform _nextButtonMobile;
    [SerializeField] private RectTransform _adButtonDesktop;
    [SerializeField] private RectTransform _adButtonMobile;

    public RectTransform WinPanel => _winPanel;
    public RectTransform WinTitle => _winTitle;
    public RectTransform ADButtonDesktop => _adButtonDesktop;
    public RectTransform ADButtonMobile => _adButtonMobile;
    public Image Dimed => _dimed;
    public RectTransform NewItem => _newItem;
    public RectTransform NextButtonDesktop => _nextButtonDesktop;
    public RectTransform NextButtonMobile => _nextButtonMobile;
}
