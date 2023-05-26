using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewLevelNotificator : MonoBehaviour
{
    [Header("MenuManager")] [SerializeField]
    private MenuManager _menuManager;
    [Header("New Level Data")]
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PopUp _newLevelPopup;
    // [SerializeField] private GameObject _perkIcon;
    [SerializeField] private Image _perkIconImage;
    [SerializeField] private TMP_Text _perkTitle;
    [SerializeField] private TMP_Text _perkInfo;
    [SerializeField] private Image[] _perkIcons;
    [SerializeField] private string[] _perkTitles;
    [SerializeField] private string[] _perkInfos;

    private void OnEnable()
    {
        _playerData.NewLevelAchieved += OnNewLevelAchieved;
    }

    private void OnDisable()
    {
        _playerData.NewLevelAchieved -= OnNewLevelAchieved;
    }

    private void OnNewLevelAchieved(int level)
    {
        _perkTitle.text = _perkTitles[level];
        _perkInfo.text = _perkInfos[level];
        _perkIconImage.sprite = _perkIcons[level].sprite;
        _perkIconImage.color = _perkIcons[level].color;
        _menuManager.ShowWithAnimation(_newLevelPopup);
    }
}
