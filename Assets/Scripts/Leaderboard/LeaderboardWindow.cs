using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using Agava.YandexGames;
using UnityEngine.InputSystem.LowLevel;
[ExecuteInEditMode]
public class LeaderboardWindow : MonoBehaviour
{
    private const int MaxLeadersRows = 8;
    
    [SerializeField] private TMP_Text[] _ranks;
    [SerializeField] private TMP_Text[] _names;
    [SerializeField] private TMP_Text[] _scores;

    [Header("Manual writing data")] 
    [SerializeField] private int _leadersNumber;

    private string _leaderboardName = "Money";
    private int _playerScore = 0;

    private void OnEnable()
    {
        // OpenYandexLeaderboard();
        FillDataTesting();
    }

    private void FillDataTesting()
    {
        int placeCounter = 1;
        int scoreCounter = 10000;
        
        for (int i = 0; i < _leadersNumber; i++)
        {
            _ranks[i].text = placeCounter.ToString();
            placeCounter++;
            _names[i].text = "Tryer";
            _scores[i].text = scoreCounter.ToString();
            scoreCounter -= Random.Range(658, 2378);
        }
    }

    private void OpenYandexLeaderboard()
    {
        Debug.Log("0 point");
        PlayerAccount.RequestPersonalProfileDataPermission();

        Debug.Log("1 point");
        if (PlayerAccount.IsAuthorized == false)
        {
            PlayerAccount.Authorize();
            Debug.Log("2 point");
        }

        Leaderboard.GetEntries(_leaderboardName, (result) =>
        {
            
            Debug.Log("3 point");
            int leadersNumber = result.entries.Length >= _names.Length ? _names.Length : result.entries.Length;

            for (int i = 0; i < leadersNumber; i++)
            {
                string name = result.entries[i].player.publicName;

                if (string.IsNullOrEmpty(name))
                {
                    name = "Anonymous";
                }

                _names[i].text = name;
                _scores[i].text = result.entries[i].formattedScore;
                _ranks[i].text = result.entries[i].rank.ToString();
            }
            Debug.Log("4 point");
        });
    }

    private void SetLeaderboardScore()
    {
        if (YandexGamesSdk.IsInitialized)
        {
            Leaderboard.GetPlayerEntry(_leaderboardName, OnSuccessCallback);
        }
    }

    private void OnSuccessCallback(LeaderboardEntryResponse result)
    {
        if (result == null || _playerScore > result.score)
        {
            Leaderboard.SetScore(_leaderboardName, _playerScore);
        }
    }
    
    
}
