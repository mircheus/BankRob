using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preparing : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;
    
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
