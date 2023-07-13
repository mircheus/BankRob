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

    private string _leaderboardName = "TopRobbers";
    private int _playerScore = 0;

    public string LeaderboardName => _leaderboardName;
    
    private void Start()
    {
        DisableAllRecords();
#if UNITY_WEBGL && !UNITY_EDITOR
        LoadYandexLeaderboard();
#endif
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
        }
    }
    
    private void OnSuccessCallback(LeaderboardEntryResponse result)
    {
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
}
