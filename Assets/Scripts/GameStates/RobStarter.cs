using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Screen = UnityEngine.Device.Screen;

public class RobStarter : MonoBehaviour
{
    [SerializeField] private DragAndDrop _dragAndDrop;
    [SerializeField] private Preparing _preparing;
    [SerializeField] private Robbery _robbery;
    [SerializeField] private GameObject _downCollider;
    [SerializeField] private ScreenAdaptation _screenAdaptation;
    [SerializeField] private Grid _grid;

    public event UnityAction Started;
    public event UnityAction NotEnoughRobbers;

    public void TryStartRob()
    {
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
        foreach (var slot in _grid.Slots)
        {
            if (slot.IsFilled)
            {
                slot.Robber.SetColumnIndex(slot.ColumnIndex);
                slot.Robber.ActivateMovement();
                slot.Robber.transform.SetParent(null, true);
                _downCollider.SetActive(false);
                _robbery.AddActiveRobber(slot.Robber);
            }
        }

        _dragAndDrop.enabled = false;
        _screenAdaptation.enabled = false;
        Started?.Invoke();
    }

    private bool IsNoDraggingActive()
    {
        return _dragAndDrop.IsDragging == false;
    }
}
