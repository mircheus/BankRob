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
    [SerializeField] private Color[] _levelColors = new [] { Color.yellow , Color.green, Color.blue, Color.magenta, Color.red, Color.white, };
    [SerializeField] private DeactivatedRobbers _deactivatedRobbers;
    
    private Image _image;
    private RobberDragger _robberDragger;
    private Robber _robber;
    private bool _isFilled;
    private Vector3 _offset = new Vector3(0f, -3.5f, 0);
    
    public bool IsFilled => _isFilled;
    public Robber Robber => _robber;
    public int ColumnIndex => _columnIndex;

    public event UnityAction<int> RobbersCombined;
    
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent(out RobberDragger robberDragger) && _isFilled == false)
        {
            robberDragger.SetLastParentTransform(transform);

            if (robberDragger.IsDraggingNow == false)
            {
                SetRobber(robberDragger.GetComponent<Robber>());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Robber externalRobber) && _isFilled)
        {
            if (_robber.Level == externalRobber.Level && _robber.Level < _robber.MaxLevel && externalRobber.GetComponent<RobberDragger>().IsDraggingNow)
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

    public void SetRobber(Robber robber)
    {
        _robber = robber;
        _robberDragger = _robber.GetComponent<RobberDragger>();
        PlaceRobberInCellCenter(_robberDragger);
        _robberDragger.SetOffset(_offset);
        _robberDragger.SetLastParentTransform(gameObject.transform);
        SetTargetForRobber(_robber);
        _robber.gameObject.SetActive(true);
        _isFilled = true;
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
        externalRobberDragger.gameObject.transform.SetParent(_deactivatedRobbers.transform);
        int level = _robber.UpgradeLevel();
        RobbersCombined?.Invoke(level);
        PlayCombineFx(level);
    }

    private void SetTargetForRobber(Robber robber)
    {
        robber.RobberMovement.SetTarget(_downTarget);
        // robber.GetComponent<RobberMovement>().SetDownTarget(_downTarget); // todo: remove comment
    }
    
    private void PlayCombineFx(int level)
    {
        level--;
        var main = _combineFx.main;
        main.startColor = _levelColors[level];
        _combineFx.Play();
    }
}
