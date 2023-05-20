using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelMapGenerator
{
    private const int ColumnsAmount = 4;

    private Barrier[] _obstacles;
    private Barrier[] _traps;

    public LevelMapGenerator(Barrier[] obstacles, Barrier[] traps)
    {
        _obstacles = obstacles;
        _traps = traps;
    }

    public Barrier[,] GetLevelMap(int floorsAmount, int obstaclesQuantity, int obstaclesLevel, int trapsQuantity, int trapsLevel)
    {
        Barrier[,] levelMap = new Barrier[floorsAmount, ColumnsAmount];
        List<string> randomCellsIndexes = GenerateRandomCells(obstaclesQuantity + trapsQuantity, floorsAmount - 1);
        levelMap = FillLevelInRandomCells(levelMap, randomCellsIndexes, obstaclesQuantity, obstaclesLevel, trapsQuantity, trapsLevel);

        return levelMap;
    }
    
    private Barrier[,] FillLevelInRandomCells(Barrier[,] availableCells, List<string> cellsIndexes,int obstaclesQuantity, int obstaclesLevel, int trapsQuantity, int trapsLevel)
    {
        int i = 0;
        int j = 0;

        for (int k = 0; k < obstaclesQuantity; k++)
        {
            GetNewRandomIndex(ref i, ref j, cellsIndexes);
            availableCells[i, j] = GetRandomObstacle(obstaclesLevel);
        }

        for (int k = 0; k < trapsQuantity; k++)
        {
            GetNewRandomIndex(ref i , ref j, cellsIndexes);
            availableCells[i, j] = GetRandomTrap(trapsLevel);
        }

        return availableCells;
    }

    private List<string> GenerateRandomCells(int cellsAmount, int rowsAmount)
    {
        List<string> randomCells = new List<string>();
        int i = -1;
        int j = -1;
        string result; 
        
        for (int k = 0; k < cellsAmount; k++)
        {
            do
            {
                i = Random.Range(0, rowsAmount);
                j = Random.Range(0, ColumnsAmount);
                result = Convert.ToString(i) + Convert.ToString(j);
            } while (randomCells.Contains(result));

            randomCells.Add(result);
        }

        return randomCells;
    }
    
    private Barrier GetRandomObstacle(int maxLevelObstacle)
    {
        return _obstacles[GenerateRandomIndex(maxLevelObstacle)]; // Magic Number
    }

    private Barrier GetRandomTrap(int maxLevelTrap) 
    {
        return _traps[GenerateRandomIndex(maxLevelTrap)]; // Magic number 
    }
    
    private int GenerateRandomIndex(int maxLevel)
    {
        return Random.Range(0, maxLevel);
    }
    
    private void GetNewRandomIndex(ref int i, ref int j, List<string> randomCellsIndexes)
    {
        string index = randomCellsIndexes[0];
        randomCellsIndexes.RemoveAt(0);
        i = (int)char.GetNumericValue(index[0]);
        j = (int)char.GetNumericValue(index[1]);
    }
}
    