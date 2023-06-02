using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

public class LeaderboardLoader : MonoBehaviour
{
    private const int MaxRecordsToShow = 10;
    
    [SerializeField] private Record[] _records;

    [Header("Debug")] 
    [SerializeField] private int _leadersToShowAmount;

    private string _leaderboardName = "Money";
    private int _playerScore = 0;

    
    
    private void Start()
    {
        DisableAllRecords();
        // FillLeaderBoardTesting(_leadersToShowAmount);
#if UNITY_WEBGL && !UNITY_EDITOR
        LoadYandexLeaderboard();
#endif
    }

    private void FillLeaderBoardTesting(int leadersAmount)
    {
        int recordsToShow = leadersAmount <= MaxRecordsToShow ? leadersAmount : MaxRecordsToShow;
        
        for (int i = 0; i < recordsToShow; i++)
        {
            _records[i].SetName("TEST");
            _records[i].SetScore("1212");
            _records[i].gameObject.SetActive(true);
        }
    }

    private void DisableAllRecords()
    {
        foreach (var record in _records)
        {
            record.gameObject.SetActive(false);
        }
    }

    private void LoadYandexLeaderboard()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();

        if (PlayerAccount.IsAuthorized == false)
        {
            PlayerAccount.Authorize();
        }
        
        Leaderboard.GetEntries(_leaderboardName, (result) =>
            {
                int recordsToShow =
                    result.entries.Length <= MaxRecordsToShow ? result.entries.Length : MaxRecordsToShow;

                for (int i = 0; i < recordsToShow; i++)
                {
                    string name = result.entries[i].player.publicName;

                    if (string.IsNullOrEmpty(name))
                    {
                        name = "Anonymous";
                    }
                    
                    _records[i].SetName(name);
                    _records[i].SetScore(result.entries[i].formattedScore);
                    _records[i].gameObject.SetActive(true);
                }
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
