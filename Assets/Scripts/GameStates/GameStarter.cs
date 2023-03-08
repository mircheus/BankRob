using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private List<Slot> _slots;
    [SerializeField] private DragAndDrop _dragAndDrop;

    public event UnityAction RobStarted;
    
    public void StartRob()
    {
        foreach (var slot in _slots)
        {
            if (slot.IsFilled)
            {
                slot.GetComponentInChildren<Robber>().ActivateMovement();
                Debug.Log("Activated");
            }
        }

        _dragAndDrop.enabled = false;
        RobStarted?.Invoke();
    }
}
