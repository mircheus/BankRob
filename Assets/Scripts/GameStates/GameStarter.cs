using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private List<GridSlot> _slots;

    public event UnityAction RobStarted;
    
    public void StartRob()
    {
        RobStarted?.Invoke();
        
        foreach (var slot in _slots)
        {
            if (slot.IsFilled)
            {
                slot.GetComponentInChildren<Robber>().ActivateMovement();
                Debug.Log("Activated");
            }
        }
    }
}
