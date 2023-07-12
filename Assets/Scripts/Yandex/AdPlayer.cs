using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class AdPlayer : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private AudioMuteHandler _audioMuteHandler;

    private bool _adIsPlaying;

    public bool AdIsPlaying => _adIsPlaying;
    
    public event UnityAction VideoAdPlayed;
    
#if UNITY_WEBGL && !UNITY_EDITOR
    private void Start()
    {
        PlayRegularAdIf(ShouldPlayAd());
    }
#endif
    
    public void OnShowVideoButtonClick()
    {   
        // VideoAdPlayed?.Invoke();
#if UNITY_WEBGL && !UNITY_EDITOR
        VideoAd.Show(OnPlayed, OnRewarded,OnClosed);
#endif
    }

    private void OnRewarded()
    {
        VideoAdPlayed?.Invoke();
    }

    private void OnClosed()
    {
        _audioMuteHandler.UnmuteAudio();
        _adIsPlaying = false;
    }

    private void OnPlayed()
    {
        _audioMuteHandler.MuteAudio();
        _adIsPlaying = true;
    }

    private void ShowInterstitialAd()
    {
        InterstitialAd.Show(OnPlayed, OnClosedInterstitialAd);
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

    private void OnClosedInterstitialAd(bool value)
    {
        _audioMuteHandler.UnmuteAudio();
        _adIsPlaying = false;
    }
}
