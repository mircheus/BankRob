using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RobStarter : MonoBehaviour
{
    [SerializeField] private List<Slot> _slots;
    [SerializeField] private DragAndDrop _dragAndDrop;
    [SerializeField] private Preparing _preparing;
    [SerializeField] private Robbery _robbery;
    [SerializeField] private PlayerData playerData;

    public event UnityAction Started;
    public event UnityAction NotEnoughRobbers;
    
    public void TryStartRob()
    {
        if (_preparing.GetRobbersQuantity() < _robbery.TargetQuantity)
        {
            NotEnoughRobbers?.Invoke();
        }
        else
        {
            StartRob();
        }
    }
    
    private void StartRob()
    {
        foreach (var slot in _slots)
        {
            if (slot.IsFilled)
            {
                slot.GetComponentInChildren<Robber>().ActivateMovement();
                playerData.SubscribeToKeyCollector(slot);
            }
        }

        _dragAndDrop.enabled = false;
        Started?.Invoke();
    }
}
