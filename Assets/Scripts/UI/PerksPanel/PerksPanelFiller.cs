using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PerksPanelFiller : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;
    [SerializeField] private RobStarter _robStarter;
    [SerializeField] private GameObject[] _perkSlots;
    [SerializeField] private GameObject _perkPrefab;
    [SerializeField] private GameObject[] _perkButtonPrefabs;
    [SerializeField] private Robbery _robbery;

    private List<GameObject> _perkButtons = new List<GameObject>();

    public event UnityAction<int> PerkActivated; 

    private void OnEnable()
    {
        _robStarter.Started += OnStarted;
        _robbery.BankRobbed += OnBankRobbed;
        _robbery.BankNotRobbed += OnBankNotRobbed;
    }

    private void OnDisable()
    {
        _robStarter.Started -= OnStarted;
    }

    private void OnStarted()
    {
        FillPerkSlots();
    }

    private void FillPerkSlots()
    {
        int[] perkLevels = GetFilledSlotsIndex();

        for (int i = 0; i < _perkSlots.Length; i++)
        {
            if (perkLevels[i] != -1)
            {
                PlacePerkToSlotWithIndex(i, perkLevels[i]);
            }
        }
    }

    private void PlacePerkToSlotWithIndex(int slotIndex, int perkLevel)
    {
        GameObject perkButton = Instantiate(_perkButtonPrefabs[perkLevel], _perkSlots[slotIndex].transform);
        perkButton.GetComponent<PerkActivator>().SetColumnIndex(slotIndex);
        perkButton.GetComponent<PerkActivator>().PerkActivated += OnPerkActivated;
        _perkButtons.Add(perkButton);
    }

    private void OnPerkActivated(int index)
    {
        PerkActivated?.Invoke(index);
    }

    private int[] GetFilledSlotsIndex()
    {
        int[] filledIndexes = new int[4];
        
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i].IsFilled)
            {
                filledIndexes[i] = _slots[i].Robber.Level - 1;
            }
            else
            {
                filledIndexes[i] = -1;
            }
        }

        return filledIndexes;
    }

    private void OnBankRobbed()
    {
        UnsubscribeFromPerkActivators();
    }

    private void OnBankNotRobbed()
    {
        UnsubscribeFromPerkActivators();
    }
    
    private void UnsubscribeFromPerkActivators()
    {
        foreach (var perkButton in _perkButtons)
        {
            perkButton.GetComponent<PerkActivator>().PerkActivated -= OnPerkActivated;
        }
    }
}

