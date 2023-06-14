using System;
using System.Collections;
using System.Collections.Generic;
using Agava.WebUtility;
using Agava.YandexGames;
using UnityEngine;

public class SDK_testing : MonoBehaviour
{
    public void OnShowInterstitialButtonClick()
    {
        InterstitialAd.Show();
    }

    public void OnShowVideoButtonClick()
    {
        VideoAd.Show();
    }
}