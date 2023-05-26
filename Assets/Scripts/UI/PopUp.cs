using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] private Image _dimed;
    [SerializeField] private RectTransform _popup;

    public Image Dimed => _dimed;
    public RectTransform Popup => _popup;
}
