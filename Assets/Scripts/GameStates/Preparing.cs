using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Preparing : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;

    public event UnityAction PreparingStarted;
    
    private void Start()
    {
        PreparingStarted?.Invoke();
    }

    public int GetRobbersQuantity()
    {
        int counter = 0;
        
        foreach (var slot in _slots)
        {
            if (slot.IsFilled)
            {
                counter++;
            }
        }

        return counter;
    }
}
