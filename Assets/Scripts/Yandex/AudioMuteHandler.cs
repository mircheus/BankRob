using System;
using System.Collections;
using System.Collections.Generic;
using Agava.WebUtility;
using Agava.YandexGames;
using Unity.VisualScripting;
using UnityEngine;

public class AudioMuteHandler : MonoBehaviour
{
    [SerializeField] private AdPlayer _adPlayer;
    
    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnBackgroundChange;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnBackgroundChange;
    }

    private void OnBackgroundChange(bool inBackground)
    {
        if (_adPlayer.AdIsPlaying == false) 
        {
            AudioListener.pause = inBackground;
            AudioListener.volume = inBackground ? 0f : 1f;
        }
    } 
}