using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class KeyCollector : MonoBehaviour
{
    public event UnityAction KeyCollected;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Key key))
        {
            KeyCollected?.Invoke();
            other.transform.gameObject.SetActive(false);
        }
    }
}
