using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class TargetMovement : MonoBehaviour
{
    [SerializeField] private RobStarter _robStarter;
    [SerializeField] private Robbery _robbery;
    
    private int _currentIndex;

    [Header("Debug")]
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;
    [SerializeField] private float _deltaY;
    [SerializeField] private List<Robber> _robbers;
    [SerializeField] private Robber _robberToFollow;
    
    public float DeltaY => _deltaY;
    
    private void OnEnable()
    {
        _robStarter.Started += OnRobStarterStarted;
    }

    private void OnDisable()
    {
        _robStarter.Started -= OnRobStarterStarted;
    }

    private void Start()
    {
        _minY = transform.position.y;
        _maxY = transform.position.y;
        _currentIndex = 0;
    }

    private void Update()
    {
        if (_robberToFollow != null)
        {
            Follow();
            CalculateDelta();
            _robberToFollow = FindLowestRobber();
        }
    }

    private void Follow()
    {
        var transform1 = transform;
        var position = transform1.position;
        position = new Vector3(position.x, _robberToFollow.transform.position.y, position.z);
        transform1.position = position;
    }
    
    private void OnRobStarterStarted()
    {
        // _robber = _robStarter.PickRobber();
        _robbers = _robbery.SendRobbersListTo(this);
        _robberToFollow = _robbers[_currentIndex];

        // foreach (Robber robber in _robbers)
        // {
        //     robber.GetComponent<RobberMovement>().GetStopped += OnGetStopped;
        // }
    }

    private void CalculateDelta()
    {
        _minY = FindMinY(_robbers);
        _maxY = FindMaxY(_robbers);
        _deltaY = Mathf.Abs(_maxY - _minY);
    }
    
    private float FindMinY(List<Robber> robbers)
    {
        float[] yCoordinates = CollectYCoordinatesFrom(robbers);
        
        if (yCoordinates == null)
        {
            return -1;
        }
        
        float minY = yCoordinates[0];

        foreach (var yCoordinate in yCoordinates)
        {
            if (yCoordinate < minY)
            {
                minY = yCoordinate;
            }
        }

        return minY + 1000f; // MAgic number
    }

    private float FindMaxY(List<Robber> robbers)
    {
        float[] yCoordinates = CollectYCoordinatesFrom(robbers);
        
        if (yCoordinates == null)
        {
            return -1;
        }
        
        float maxY = yCoordinates[0];

        foreach (var yCoordinate in yCoordinates)
        {
            if (yCoordinate > maxY)
            {
                maxY = yCoordinate;
            }
        }

        return maxY + 1000f; // Magic Number
    }

    private float[] CollectYCoordinatesFrom(List<Robber> robbers)
    {
        if (robbers.Count == 0)
        {
            return null;
        }
        
        float[] yCoordinates = new float[robbers.Count];

        for (int i = 0; i < robbers.Count; i++)
        {
            yCoordinates[i] = robbers[i].transform.position.y;
        }

        return yCoordinates;
    }

    private void OnGetStopped()
    {
        // Debug.Log("OnGetStopped From target movement");
    }

    private Robber FindLowestRobber()
    {
        if (_robbers.Count == 0)
        {
            return null;
        }
        
        Robber lowestRobber = _robbers[0];
        
        foreach (var robber in _robbers)
        {
            if (robber.transform.position.y < lowestRobber.transform.position.y)
            {
                lowestRobber = robber;
            }    
        }

        return lowestRobber;
    }
}
