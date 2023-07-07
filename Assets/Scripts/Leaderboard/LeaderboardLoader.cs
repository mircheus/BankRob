using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

public class LeaderboardLoader : MonoBehaviour
{
    private const int MaxRecordsToShow = 10;
    private const string Anonymous = "Anonymous";
    
    [SerializeField] private Record[] _records;
    [SerializeField] private PlayerRecord _playerRecord;

    [Header("Debug")] 
    [SerializeField] private int _leadersToShowAmount;

    private string _leaderboardName = "TopRobbers";
    private int _playerScore = 0;

    public string LeaderboardName => _leaderboardName;
    
    private void Start()
    {
        DisableAllRecords();
        // LoadPlayerScoreTest();
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
        _playerRecord.gameObject.SetActive(false);
        
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
        // Debug.Log($"LeaderboardName: {_leaderboardName}");
        Leaderboard.GetEntries(_leaderboardName, (result) =>
            {
                int recordsToShow =
                    result.entries.Length <= MaxRecordsToShow ? result.entries.Length : MaxRecordsToShow;

                for (int i = 0; i < recordsToShow; i++)
                {
                    string name = result.entries[i].player.publicName;

                    if (string.IsNullOrEmpty(name))
                    {
                        name = Anonymous;
                    }
                    
                    _records[i].SetName(name);
                    _records[i].SetScore(result.entries[i].formattedScore);
                    _records[i].gameObject.SetActive(true);
                }
            });
        
        LoadPlayerScore();
    }

    private void LoadPlayerScore()
    {
        if (YandexGamesSdk.IsInitialized)
        {
            Leaderboard.GetPlayerEntry(_leaderboardName, OnSuccessCallback);
            // Debug.Log($"LeaderboardName: {_leaderboardName}");
        }
    }
    
    private void OnSuccessCallback(LeaderboardEntryResponse result)
    {
        // if (result == null || _playerScore > result.score)
        // {
        //     Leaderboard.SetScore(_leaderboardName, _playerScore);
        // }

        if (result != null)
        {
            _playerRecord.gameObject.SetActive(true);
            _playerRecord.SetName(result.player.publicName);
            _playerRecord.SetScore(result.score.ToString());
            _playerRecord.SetRank(result.rank);
        }
        else
        {
            _playerRecord.gameObject.SetActive(false);
        }
    }

    private void LoadPlayerScoreTest()
    {
        _playerRecord.SetName(Anonymous);
        _playerRecord.SetScore(1488.ToString());
        _playerRecord.SetRank(6);
    }

}
