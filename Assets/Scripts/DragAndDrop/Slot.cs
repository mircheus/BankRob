using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Slot : MonoBehaviour
{
    [SerializeField] private Transform _downTarget;
    
    private RobberDragger _robberDragger;
    private Robber _robber;
    private bool _isFilled;
    
    public bool IsFilled => _isFilled;
    public Robber Robber => _robber;

    public event UnityAction RobbersCombined;
    
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent(out RobberDragger robberDragger) && _isFilled == false)
        {
            robberDragger.SetLastParentTransform(transform);
            
            if (robberDragger.IsDraggingNow == false)
            {
                _robberDragger = robberDragger;
                _robber = _robberDragger.GetComponent<Robber>();
                SetDownTargetForRobber(_robber);
                PlaceRobberInCellCenter(robberDragger);
                _isFilled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Robber externalRobber) && _isFilled)
        {
            if (_robber.Level == externalRobber.Level)
            {
                CombineRobbers(externalRobber);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out RobberDragger robberDragger))
        {
            _robberDragger = null;
            _isFilled = false;
        }
    }

    public void Unfill()
    {
        _isFilled = false;
    }

    private void PlaceRobberInCellCenter(RobberDragger robberDragger)
    {
        Transform robberTransform = robberDragger.transform;
        robberTransform.SetParent(gameObject.transform);
        robberTransform.position = transform.position;
    }

    private void CombineRobbers(Robber externalRobberDragger)
    {
        externalRobberDragger.gameObject.SetActive(false);
        _robber.UpgradeLevel();
        RobbersCombined?.Invoke();
    }

    private void SetDownTargetForRobber(Robber robber)
    {
        robber.GetComponent<RobberMovement>().SetDownTarget(_downTarget);
    }
}
