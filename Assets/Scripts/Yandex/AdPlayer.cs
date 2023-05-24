using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class AdPlayer : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    
    public event UnityAction VideoAdPlayed;

    private void Start()
    {
        PlayRegularAdIf(ShouldPlayAd());
    }

    public void OnShowVideoButtonClick()
    {
        VideoAdPlayed?.Invoke();
        VideoAd.Show();
    }

    private void ShowInterstitialAd()
    {
        InterstitialAd.Show();
    }

    private void PlayRegularAdIf(bool value)
    {
        if (value)
        {
            ShowInterstitialAd();
        }
    }

    private bool ShouldPlayAd()
    {
        return _playerData.CompletedLevelsCounter % 2 == 0;
    }
}
