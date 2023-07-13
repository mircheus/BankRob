using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BarriersProgression : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    [Header("Progression settings")] 
    [SerializeField] private int _difficultyFactor;
    [SerializeField] private int _obstaclesReducer;
    [SerializeField] private int _trapsAppearsOnLevel;
    [SerializeField] private int _trapsReducer;

    [Header("Obstacles")] 
    [SerializeField] private Barrier[] _obstacles;
    [SerializeField] private int _firstObstacleUpgrade;
    [SerializeField] private int _secondObstacleUpgrade;
    [SerializeField] private int _thirdObstacleUpgrade;

    [Header("Traps")] 
    [SerializeField] private Barrier[] _traps;
    [SerializeField] private int _firstUpgradeOnLevel;
    [SerializeField] private int _secondUpgradeOnLevel;
    [SerializeField] private int _thirdUpgradeOnLevel;
    
    [Header("Level settings")]
    [SerializeField] private int _firstLevelFloorsAmount;

    public event UnityAction<Barrier[,]> LevelMapPrepared; 
    
    private int _levelsCounter;
    private int _floorsQuantity;
    private int _obstaclesQuantity;
    private int _obstaclesLevel;
    private int _keysQuantity;
    private int _keysFromPreviousLevel;
    private int _trapsLevel;
    private int _trapsQuantity;

    public int FloorsQuantity => _floorsQuantity;
    public int ObstaclesQuantity => _obstaclesQuantity;
    public int ObstaclesLevel => _obstaclesLevel;
    public int TrapsQuantity => _trapsQuantity;
    public int TrapsLevel => _trapsLevel;
    public int FirstLevelFloorsAmount => _firstLevelFloorsAmount;

    private void OnEnable()
    {
        _playerData.DataLoaded += OnDataLoaded;
    }

    private void OnDisable()
    {
        _playerData.DataLoaded -= OnDataLoaded;
    }

    private void OnDataLoaded()
    {
        _levelsCounter = _playerData.CompletedLevelsCounter;
        _floorsQuantity = CalculateFloorsQuantity(_levelsCounter, _playerData.FloorsAmountFromPreviousLevel);
        _trapsQuantity = CalculateTrapsQuantity(_levelsCounter, _floorsQuantity);
        _obstaclesQuantity = CalculateObstaclesQuantity(_floorsQuantity, _trapsQuantity);
        _obstaclesLevel = SetObstaclesLevel(_levelsCounter);
        _trapsLevel = SetTrapsLevel(_levelsCounter);
        LevelMapGenerator levelMapGenerator = new LevelMapGenerator(_obstacles, _traps);
        Barrier[,] levelMap = levelMapGenerator.GetLevelMap(_floorsQuantity, _obstaclesQuantity, _obstaclesLevel, _trapsQuantity, _trapsLevel);
        LevelMapPrepared?.Invoke(levelMap);
    }

    private int CalculateFloorsQuantity(int levelsPassed, int floorsAmountFromPreviousLevel) 
    {
        if (_playerData.IsTryAgain)
        {
            return floorsAmountFromPreviousLevel;
        }
        
        if (levelsPassed % 2 == 0)
        {
            int increasedFloorsAmount = floorsAmountFromPreviousLevel + 1;
            return increasedFloorsAmount;
        }

        return floorsAmountFromPreviousLevel;
    }

    private int CalculateObstaclesQuantity(int currentFloorsAmount, int trapsAmount)
    {
        return (currentFloorsAmount - 1) * _difficultyFactor - trapsAmount - _obstaclesReducer; 
    }

    private int SetObstaclesLevel(int levelsPassed) // workaround
    {
        if (levelsPassed >= _thirdObstacleUpgrade)
        {
            return 4;
        }

        if (levelsPassed >= _secondObstacleUpgrade)
        {
            return 3;
        }

        return 2;
    }

    private int CalculateTrapsQuantity(int levelsPassed, int currentFloorsAmount) 
    {
        if (levelsPassed >= _trapsAppearsOnLevel)
        {
            return currentFloorsAmount - _trapsReducer;
        }

        return 0;
    }

    private int SetTrapsLevel(int levelsPassed) // workaround
    {
        if (levelsPassed >= _thirdUpgradeOnLevel)
        {
            return 3;
        }
        
        if (levelsPassed >= _secondUpgradeOnLevel)
        {
            return 2;
        }

        if (levelsPassed >= _firstUpgradeOnLevel)
        {
            return 1;
        }

        return 0;
    }
}
