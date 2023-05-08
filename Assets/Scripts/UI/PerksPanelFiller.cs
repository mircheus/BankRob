using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerksPanelFiller : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;
    [SerializeField] private RobStarter _robStarter;
    [SerializeField] private GameObject[] _perkSlots; // WORKAROUND
    [SerializeField] private GameObject _perkPrefab;

    private void OnEnable()
    {
        _robStarter.Started += OnStarted;
    }

    private void OnDisable()
    {
        _robStarter.Started -= OnStarted;
    }

    private void OnStarted()
    {
        Debug.Log("OnStarted");
        FillPerkSlots();
    }

    private void FillPerkSlots()
    {
        bool[] indexes = GetFilledSlotsIndex();

        for (int i = 0; i < _perkSlots.Length; i++)
        {
            if (indexes[i])
            {
                PlacePerkToSlotWithFollowing(i);
                Debug.Log($"Placed to {i}");
            }
        }
    }

    private void PlacePerkToSlotWithFollowing(int slotIndex)
    {
        Instantiate(_perkPrefab, _perkSlots[slotIndex].transform);
    }

    private bool[] GetFilledSlotsIndex()
    {
        bool[] filledIndexes = new bool[4];
        
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i].IsFilled)
            {
                filledIndexes[i] = true;
            }
            else
            {
                filledIndexes[i] = false;
            }
        }

        return filledIndexes;
    }
}

