using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

public class Authorization : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private MenuManager _menuManager;
    
    public void TryAuthorize()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        PlayerAccount.Authorize(OnAuthorizeSuccess, OnAuthorizeError);
#endif
        
    }

    private void OnAuthorizeSuccess()
    {
        _playerData.AuthorizeBy(this);
        _menuManager.TryOpenLeaderboard();
    }

    private void OnAuthorizeError(string error)
    {
        Debug.Log(error);
    }
}
