using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleLoader : Loader
{
    [SerializeField] private protected Obstacle[] _obstacles;
    [Header("Probability (lower - higher)")]
    [SerializeField] private int _probability;
    
    private readonly List<Vector3> _availableCells = new List<Vector3>();
    private int _obstaclesLevel;
    private int _obstaclesAmount;

    public List<Vector3> AvailableCells => _availableCells;

    protected override void SetFloorQuantity(int floorQuantity)
    {
        _floorQuantity = floorQuantity - 1;
    }

    protected override void SetCursorIn(Vector3 position, Transform parent)
    {
        if (TryGenerateObjectInPosition(position, parent) == false)
        {
            _availableCells.Add(position);
        }
    }

    protected override bool TryGenerateObjectInPosition(Vector3 position, Transform parent)
    {
        // if(_obstaclesAmount > 0)
        if (Random.Range(0, 10) > _probability && _obstaclesAmount > 0)
        {
            _obstaclesAmount--;
            int randomIndex = Random.Range(0, _obstaclesLevel);
            Instantiate(_obstacles[randomIndex], position, Quaternion.identity, parent);
            return true;
        }

        return false;
    }

    public void SetObstaclesAmount(int obstaclesAmount)
    {
        _obstaclesAmount = obstaclesAmount;
    }

    public void SetObstaclesLevel(int obstaclesLevel)
    {
        _obstaclesLevel = obstaclesLevel;
    }
}
