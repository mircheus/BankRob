using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RobStarter : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;
    [SerializeField] private DragAndDrop _dragAndDrop;
    [SerializeField] private Preparing _preparing;
    [SerializeField] private Robbery _robbery;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private GameObject _downCollider;
    // [SerializeField] private Grid _grid;

    // private Robber _savedRobber;
    
    [Header("DEBUG")]
    [SerializeField] private int _howManyRobbers = 0;
    
    public event UnityAction Started;
    public event UnityAction NotEnoughRobbers;

    public void TryStartRob()
    {
        // if (_preparing.GetRobbersQuantity() > 0)
        if (_preparing.GetRobbersQuantity() > 0 && IsNoDraggingActive())
        {
            StartRob();
        }
        else
        {
            NotEnoughRobbers?.Invoke();
        }
    }
    
    private void StartRob()
    {
        foreach (var slot in _slots) // перенести в Grid класс
        {
            if (slot.IsFilled)
            {
                slot.Robber.SetColumnIndex(slot.ColumnIndex);
                // slot.PlaceRobberInCellCenter(slot.Robber.GetComponent<RobberDragger>());
                slot.Robber.ActivateMovement();
                slot.Robber.GetComponent<AnimationSwitcher>().PlayFirstAttack();
                _playerData.SubscribeToKeyCollector(slot.Robber);
                // _savedRobber = slot.Robber;
                slot.Robber.transform.SetParent(null, true);
                _downCollider.SetActive(false);
                _howManyRobbers++;
                _robbery.AddActiveRobber(slot.Robber);
            }
        }

        _dragAndDrop.enabled = false;
        Started?.Invoke();
    }

    private bool IsNoDraggingActive()
    {
        return _dragAndDrop.IsDragging == false;
    }
}
