using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FreezedButton : MonoBehaviour
{
    [SerializeField] private int _health = 3;

    public event UnityAction Unfreezed;
    
    public void OnClick()
    {
        _health--;

        if (_health <= 0)
        {
            UnfreezeButton();
        }
    }

    private void UnfreezeButton()
    {
        gameObject.transform.SetSiblingIndex(1);
        gameObject.SetActive(false);
        Unfreezed?.Invoke();
    }
}
