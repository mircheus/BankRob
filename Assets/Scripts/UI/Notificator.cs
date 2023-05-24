using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notificator : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private GameObject _newLevelPopup;
    [SerializeField] private GameObject _perkIcon;
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
        // _perkIcon = Instantiate(_perkIcons[level], _perkIcon.transform.position, Quaternion.identity, _perkIcon.transform);
        _perkIconImage.sprite = _perkIcons[level].sprite;
        _perkIconImage.color = _perkIcons[level].color;
        _newLevelPopup.SetActive(true);
    }
}
