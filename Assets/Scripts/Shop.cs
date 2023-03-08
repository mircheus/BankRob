using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<GridSlot> _slots;
    [SerializeField] private Robber _robberPrefab;
    private List<Robber> _robbers = new List<Robber>();
    
    private void Start()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            var robber = Instantiate(_robberPrefab);
            robber.gameObject.SetActive(false);
            _robbers.Add(robber);
        }
    }

    public void BuyRobber()
    {
        var robber = _robbers.FirstOrDefault(p => p.gameObject.activeSelf == false);

        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].IsFilled == false)
            {
                robber.transform.position = _slots[i].transform.position;
                robber.gameObject.SetActive(true);
            }
        }
    }
}
