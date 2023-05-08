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
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private GameObject _downCollider;

    private Robber _savedRobber;
    
    [Header("DEBUG")]
    [SerializeField] private int _howManyRobbers = 0;
    
    public event UnityAction Started;
    public event UnityAction NotEnoughRobbers;
    
    public void TryStartRob()
    {
        if (_preparing.GetRobbersQuantity() > 0)
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
        foreach (var slot in _slots)
        {
            if (slot.IsFilled)
            {
                slot.Robber.SetColumnIndex(slot.ColumnIndex);
                slot.Robber.ActivateMovement();
                _playerData.SubscribeToKeyCollector(slot.Robber);
                _savedRobber = slot.Robber;
                slot.Robber.transform.SetParent(null, true);
                _downCollider.SetActive(false);
                _howManyRobbers++;
                _robbery.AddActiveRobber(slot.Robber);
            }
        }

        _dragAndDrop.enabled = false;
        Started?.Invoke();
    }

    public Robber PickRobber() // WORKAROUND FOR CAMERA FOLLOW TARGET
    {
        return _savedRobber;
    }
}
