using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TargetMovement : MonoBehaviour
{
    [SerializeField] private RobStarter _robStarter;
    [SerializeField] private Robbery _robbery;
    
    private Robber _robberToFollow;
    private int _currentIndex;

    [Header("Debug")]
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;
    [SerializeField] private float _deltaY;
    [SerializeField] private Robber[] _robbers;
    
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
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeRobberToFollow();
        }
    }
    
    public void ChangeRobberToFollow()
    {
        Debug.Log("Changed");
        _currentIndex++;

        if (_currentIndex == _robbers.Length)
        {
            _currentIndex = 0;
        }

        _robberToFollow = _robbers[_currentIndex];
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
        _robbers = _robbery.GetRobbersListTo(this).ToArray();
        _robberToFollow = _robbers[_currentIndex];
    }

    private float FindMinY(Robber[] robbers)
    {
        float[] yCoordinates = CollectYCoordinatesFrom(robbers);
        float minY = yCoordinates[0];

        foreach (var yCoordinate in yCoordinates)
        {
            if (yCoordinate < minY)
            {
                minY = yCoordinate;
            }
        }

        return minY + 1000f;
    }

    private void CalculateDelta()
    {
        _minY = FindMinY(_robbers);
        _maxY = FindMaxY(_robbers);
        _deltaY = Mathf.Abs(_maxY - _minY);
    }

    private float FindMaxY(Robber[] robbers)
    {
        float[] yCoordinates = CollectYCoordinatesFrom(robbers);
        float maxY = yCoordinates[0];

        foreach (var yCoordinate in yCoordinates)
        {
            if (yCoordinate > maxY)
            {
                maxY = yCoordinate;
            }
        }

        return maxY + 1000f;
    }

    private float[] CollectYCoordinatesFrom(Robber[] robbers)
    {
        float[] yCoordinates = new float[robbers.Length];

        for (int i = 0; i < robbers.Length; i++)
        {
            yCoordinates[i] = robbers[i].transform.position.y;
        }

        return yCoordinates;
    }
}
