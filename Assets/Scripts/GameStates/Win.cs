using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private List<Slot> _slots;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Robbery _robbery;

    private void OnEnable()
    {
        _robbery.BankRobbed += OnBankRobbed;
    }

    private void OnDisable()
    {
        _robbery.BankRobbed -= OnBankRobbed;
    }

    public void OnBankRobbed()
    {
        foreach (var slot in _slots)
        {
            playerData.UnsubscribeFromKeyCollector(slot);
        }
    }
}
