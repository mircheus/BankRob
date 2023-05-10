using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class Slot : MonoBehaviour
{
    [SerializeField] private Transform _downTarget;
    [SerializeField] private int _columnIndex;
    [SerializeField] private ParticleSystem _combineFx;

    private Image _image;
    private RobberDragger _robberDragger;
    private Robber _robber;
    private bool _isFilled;
    private Vector3 _offset = new Vector3(0f, -3.5f, 0);
    
    public bool IsFilled => _isFilled;
    public Robber Robber => _robber;
    public int ColumnIndex => _columnIndex;

    public event UnityAction RobbersCombined;
    
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent(out RobberDragger robberDragger) && _isFilled == false)
        {
            robberDragger.SetLastParentTransform(transform);
            Debug.Log("stay");

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

    public void PlaceNewRobber(Robber robber)
    {
        PlaceRobberInCellCenter(robber.GetComponent<RobberDragger>());
    }

    private void PlaceRobberInCellCenter(RobberDragger robberDragger)
    {
        Transform robberTransform = robberDragger.transform;
        robberTransform.SetParent(gameObject.transform);
        robberTransform.position = transform.position + _offset;
    }

    private void CombineRobbers(Robber externalRobberDragger)
    {
        externalRobberDragger.gameObject.SetActive(false);
        _robber.UpgradeLevel();
        RobbersCombined?.Invoke();
        _combineFx.Play();
    }

    private void SetDownTargetForRobber(Robber robber)
    {
        robber.GetComponent<RobberMovement>().SetDownTarget(_downTarget);
    }
}
