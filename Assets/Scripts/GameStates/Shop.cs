using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;
    [SerializeField] private Robber _robberPrefab;
    [SerializeField] private RobbersPool _robbersPool;
    
    private List<Robber> _robbers = new List<Robber>();
    
    private void Start()
    {
        InstantiateRobbers(_slots.Length);
    }

    public void BuyRobber()
    {
        var robber = _robbers.FirstOrDefault(p => p.gameObject.activeSelf == false);
        
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i].IsFilled == false)
            {
                robber.transform.position = _slots[i].transform.position;
                robber.gameObject.SetActive(true);
            }
        }
    }

    private void InstantiateRobbers(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var robber = Instantiate(_robberPrefab, _robbersPool.transform);
            robber.gameObject.SetActive(false);
            _robbers.Add(robber);
        }
    }
}
