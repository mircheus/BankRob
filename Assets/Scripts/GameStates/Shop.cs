using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Slot> _slots;
    [SerializeField] private Robber _robberPrefab;
    [SerializeField] private RobbersPool _robbersPool;
    
    private List<Robber> _robbers = new List<Robber>();
    
    private void Start()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            var robber = Instantiate(_robberPrefab, _robbersPool.transform);
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
