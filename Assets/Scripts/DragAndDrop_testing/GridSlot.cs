using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GridSlot : MonoBehaviour
{
    [SerializeField] private bool _isFilled = false;
    private RobberDragger _robberDragger;
    private Robber _robber;

    public bool IsFilled => _isFilled;
    public Robber Robber => _robber;
    
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent(out RobberDragger robberDragger) && _isFilled == false)
        {
            robberDragger.SetLastParentTransform(transform);
            
            if (robberDragger.IsDraggingNow == false)
            {
                _robberDragger = robberDragger;
                _isFilled = true;
                PlaceRobberInCellCenter(robberDragger);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Robber externalRobber) && _isFilled)
        {
            _robber = _robberDragger.GetComponent<Robber>();

            if (_robber.Level == externalRobber.Level)
            {
                CombineRobbers(externalRobber);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out RobberDragger robber))
        {
            _robberDragger = null;
            _isFilled = false;
        }
    }

    private void PlaceRobberInCellCenter(RobberDragger robberDragger)
    {
        robberDragger.transform.SetParent(gameObject.transform);
        robberDragger.transform.position = transform.position;
    }

    private void CombineRobbers(Robber externalRobberDragger)
    {
        externalRobberDragger.gameObject.SetActive(false);
        _robber.UpgradeLevel();
    }
}
