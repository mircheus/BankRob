using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private List<GridSlot> _slots;
    
    private RobberMovement _robberMovement;

    private int _counter = 0;
    
    public void StartRob()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].IsFilled)
            {
                // _slots[i].Robber.gameObject.GetComponent<RobberMovement>().enabled = true;
                // Debug.Log(_counter);
                // _counter++;
                _slots[i].GetComponentInChildren<Robber>().ActivateMovement();
                Debug.Log("Activated");
            }
        }
    }
}
