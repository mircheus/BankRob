using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleLoader : Loader
{
    [SerializeField] private protected Obstacle[] _obstacles;
    
    private readonly List<Vector3> _possibleKeyPositions = new List<Vector3>();
    private List<Obstacle> _obstaclesList = new List<Obstacle>();
    private int _obstaclesLevel;
    
    public List<Vector3> PossibleKeyPositions => _possibleKeyPositions;

    protected override void SetFloorQuantity(int floorQuantity)
    {
        _floorQuantity = floorQuantity - 1;
    }

    protected override void SetCursorIn(Vector3 position, Transform parent)
    {
        if (TryGenerateObjectInPosition(position, parent) == false)
        {
            _possibleKeyPositions.Add(position);
        }
    }

    protected override bool TryGenerateObjectInPosition(Vector3 position, Transform parent)
    {
        if (Random.Range(0, 2) == 1)
        {
            int randomIndex = Random.Range(0, _obstaclesLevel);
            Instantiate(_obstacles[randomIndex], position, Quaternion.identity, parent);
            return true;
        }

        return false;
    }

    public void SetObstaclesLevel(int obstaclesLevel)
    {
        _obstaclesLevel = obstaclesLevel;
    }
}
