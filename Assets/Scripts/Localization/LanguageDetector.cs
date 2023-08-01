using UnityEngine;
using Agava.YandexGames;

public class LanguageDetector : MonoBehaviour
{
    [SerializeField] private LocaleSelector _localeSelector;
    
    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        _localeSelector.SwitchLanguageTo(YandexGamesSdk.Environment.i18n.lang);
#endif
    }
}
