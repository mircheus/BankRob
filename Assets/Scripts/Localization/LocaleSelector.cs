using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;
using UnityEngine.SocialPlatforms;

public class LocaleSelector : MonoBehaviour
{
    private Coroutine _setLocaleCoroutine;

    public event UnityAction<int> LocaleChanged; 

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
        Debug.Log(LocalizationSettings.InitializationOperation);
        LocaleChanged?.Invoke(localeId);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeId];
    }
}
