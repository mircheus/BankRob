using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grid : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;

    public event UnityAction<int> RobbersCombined;
    
    private void OnEnable()
    {
        foreach (var slot in _slots)
        {
            slot.RobbersCombined += OnRobbersCombined;
        }
    }

    private void OnDisable()
    {
        foreach (var slot in _slots)
        {
            slot.RobbersCombined -= OnRobbersCombined;
        }
    }

    private void OnRobbersCombined(int level)
    {
        foreach (var slot in _slots)
        {
            if (slot.IsFilled && slot.Robber.gameObject.activeSelf == false)
            {
                slot.Unfill();
            }
        }
        
        RobbersCombined?.Invoke(level);
    }
}
