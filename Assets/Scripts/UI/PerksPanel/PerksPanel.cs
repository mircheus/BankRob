using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerksPanel : MonoBehaviour
{
    [SerializeField] private PerkSlot[] _perkSlots;
    [SerializeField] private Robbery _robbery;
    
    private void OnEnable()
    {
        SubscribeButtonsToRobbers(_robbery.SendRobbersListTo(this));
        // Debug.Log("Subscribed to Robbers in PerksPanel");
    }

    private void SubscribeButtonsToRobbers(Robber[] robbers)
    {
        for (int i = 0; i < _perkSlots.Length; i++)
        {
            if (robbers[i] != null)
            {
                _perkSlots[i].SubscribeToRobber(robbers[i]);
            }
        }
    }
}
