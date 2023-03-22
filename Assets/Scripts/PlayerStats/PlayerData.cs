using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    [Header("DEBUG")]
    [SerializeField] private int _keysAmount;

    public event UnityAction DataUpdated;
    
    public int KeysAmount => _keysAmount;

    public void SubscribeToKeyCollector(Slot slot)
    {
        slot.GetComponentInChildren<KeyCollector>().KeyCollected += OnKeyCollected;
    }

    public void UnsubscribeFromKeyCollector(Slot slot)
    {
        slot.GetComponentInChildren<KeyCollector>().KeyCollected -= OnKeyCollected;
    }

    private void OnKeyCollected()
    {
        _keysAmount++;
        DataUpdated?.Invoke();
    }
}
