using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class NewLevelNotificator : MonoBehaviour
{
    private const string EnglishCode = "en";
    private const string RussianCode = "ru";
    private const string TurkishCode = "tr";
    
    [Header("MenuManager")] [SerializeField]
    private MenuManager _menuManager;
    
    [Header("New Level Data")]
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PopUp _newLevelPopup;
    [SerializeField] private Image _perkIconImage;
    [SerializeField] private TMP_Text _perkTitle;
    [SerializeField] private TMP_Text _perkInfo;
    [SerializeField] private Image[] _perkIcons;
    
    [Header("Perk titles in all languages")]
    [SerializeField] private string[] _perkTitlesEng;
    [SerializeField] private string[] _perkTitlesRus;
    [SerializeField] private string[] _perkTitlesTur;
    
    [Header("Perk infos in all languages")]
    [SerializeField] private string[] _perkInfosEng;
    [SerializeField] private string[] _perkInfosRus;
    [SerializeField] private string[] _perkInfosTur;

    private string[] _currentPerkTitles;
    private string[] _currentPerkInfos;

    [Header("LocaleIdSource")] 
    [SerializeField] private LocaleSelector _localeSelector;

    private void OnEnable()
    {
        _playerData.NewLevelAchieved += OnNewLevelAchieved;
        _localeSelector.LocaleChanged += OnLocaleChanged;
    }

    private void OnDisable()
    {
        _playerData.NewLevelAchieved -= OnNewLevelAchieved;
        _localeSelector.LocaleChanged -= OnLocaleChanged;
    }

    private void Start()
    {   
        SwitchByStringCode(LocalizationSettings.SelectedLocale.Identifier.Code);
    }

    private void OnNewLevelAchieved(int level)
    {
        _perkTitle.text = _currentPerkTitles[level];
        _perkInfo.text = _currentPerkInfos[level];
        _perkIconImage.sprite = _perkIcons[level].sprite;
        _perkIconImage.color = _perkIcons[level].color;
        _menuManager.Show(_newLevelPopup);
    }

    private void OnLocaleChanged(int localeId)
    {
        switch (localeId)
        {
            case 0: 
                _currentPerkInfos = _perkInfosEng;
                _currentPerkTitles = _perkTitlesEng;
                break;
            
            case 1:
                _currentPerkInfos = _perkInfosRus;
                _currentPerkTitles = _perkTitlesRus;
                break;
            
            case 2:
                _currentPerkInfos = _perkInfosTur;
                _currentPerkTitles = _perkTitlesTur;
                break;
        }
    }

    private void SwitchByStringCode(string code)
    {
        switch (code)
        {
            case EnglishCode:
                _currentPerkInfos = _perkInfosEng;
                _currentPerkTitles = _perkTitlesEng;
                break;
            
            case RussianCode:
                _currentPerkInfos = _perkInfosRus;
                _currentPerkTitles = _perkTitlesRus;
                break;
            
            case TurkishCode:
                _currentPerkInfos = _perkInfosRus;
                _currentPerkTitles = _perkTitlesRus;
                break;
        }
    }
}
