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
    [SerializeField] private PerkActivator[] _perkButtonPrefabs;
    [SerializeField] private Robbery _robbery;

    private List<PerkActivator> _perkButtons = new List<PerkActivator>();

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
        PerkActivator perkActivator = Instantiate(_perkButtonPrefabs[perkLevel], _perkSlots[slotIndex].transform);
        perkActivator.SetColumnIndex(slotIndex);
        perkActivator.PerkActivated += OnPerkActivated;
        _perkButtons.Add(perkActivator);
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
                filledIndexes[i] = _slots[i].Robber.Level;
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
            perkButton.PerkActivated -= OnPerkActivated;
        }
    }
}

