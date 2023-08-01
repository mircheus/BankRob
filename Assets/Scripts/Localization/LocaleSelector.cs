using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;
using UnityEngine.SocialPlatforms;

public class LocaleSelector : MonoBehaviour
{
    private const string EnglishCode = "en";
    private const string RussianCode = "ru";
    private const string TurkishCode = "tr";
    private const int EnglishCodeId = 0;
    private const int RussianCodeId = 1;
    private const int TurkishCodeId = 2;
    
    private Coroutine _setLocaleCoroutine;

    public event UnityAction<int> LocaleChanged;

    public void SwitchLanguageTo(string code)
    {
        switch (code)
        {
            case EnglishCode:
                StartCoroutine(SetLocale(EnglishCodeId));
                break;
            
            case RussianCode:
                StartCoroutine(SetLocale(RussianCodeId));
                break;
            
            case TurkishCode:
                StartCoroutine(SetLocale(TurkishCodeId));
                break;
        }
    }

    public void ChangeLocale(int localeId)
    {
        if (_setLocaleCoroutine == null)
        {
            _setLocaleCoroutine = StartCoroutine(SetLocale(localeId));
        }
        else
        {
            StopCoroutine(_setLocaleCoroutine);
            _setLocaleCoroutine = StartCoroutine(SetLocale(localeId));
        }
    }
    
    private IEnumerator SetLocale(int localeId)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocaleChanged?.Invoke(localeId);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeId];
    }
}
