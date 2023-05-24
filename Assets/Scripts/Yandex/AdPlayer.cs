using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class AdPlayer : MonoBehaviour
{
    public event UnityAction VideoAdPlayed;
    
    public void OnShowVideoButtonClick()
    {
        VideoAdPlayed?.Invoke();
        VideoAd.Show();
    }
}
