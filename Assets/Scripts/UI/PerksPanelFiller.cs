using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PerksPanelFiller : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;
    [SerializeField] private RobStarter _robStarter;
    [SerializeField] private GameObject[] _perkSlots; // WORKAROUND
    [SerializeField] private GameObject _perkPrefab;

    private List<GameObject> _perkButtons = new List<GameObject>();

    public event UnityAction<int> PerkActivated; 

    private void OnEnable()
    {
        _robStarter.Started += OnStarted;
    }

    private void OnDisable()
    {
        _robStarter.Started -= OnStarted;

        foreach (var perkButton in _perkButtons)
        {
            perkButton.GetComponent<PerkActivator>().PerkActivated -= OnPerkActivated;
        }
    }

    private void OnStarted()
    {
        FillPerkSlots();
    }

    private void FillPerkSlots()
    {
        bool[] indexes = GetFilledSlotsIndex();

        for (int i = 0; i < _perkSlots.Length; i++)
        {
            if (indexes[i])
            {
                PlacePerkToSlotWithIndex(i);
            }
        }
    }

    private void PlacePerkToSlotWithIndex(int slotIndex)
    {
        GameObject perkButton = Instantiate(_perkPrefab, _perkSlots[slotIndex].transform);
        perkButton.GetComponent<PerkActivator>().SetColumnIndex(slotIndex);
        perkButton.GetComponent<PerkActivator>().PerkActivated += OnPerkActivated;
        _perkButtons.Add(perkButton);
    }

    private void OnPerkActivated(int index)
    {
        Debug.Log($"perk with index {index} activated");
        PerkActivated?.Invoke(index);
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

