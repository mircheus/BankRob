using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;

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

    private void OnRobbersCombined()
    {
        foreach (var slot in _slots)
        {
            if (slot.IsFilled && slot.Robber.gameObject.activeSelf == false)
            {
                slot.Unfill();
            }
        }
    }
}
