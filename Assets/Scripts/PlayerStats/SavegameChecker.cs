using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavegameChecker : MonoBehaviour
{
    [SerializeField] private DataManager _dataManager;

    public bool IsAnySavegamePersist()
    {
        return _dataManager.IsLoadDataPersists();
    }
}
