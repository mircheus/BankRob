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
        List<CellIndex> randomCellsIndexes = GenerateRandomCells(obstaclesQuantity + trapsQuantity, floorsAmount - 1);
        levelMap = FillLevelInRandomCells(levelMap, randomCellsIndexes, obstaclesQuantity, obstaclesLevel, trapsQuantity, trapsLevel);

        return levelMap;
    }
    
    private Barrier[,] FillLevelInRandomCells(Barrier[,] availableCells, List<CellIndex> cellsIndexes,int obstaclesQuantity, int obstaclesLevel, int trapsQuantity, int trapsLevel)
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

    private List<CellIndex> GenerateRandomCells(int cellsAmount, int rowsAmount)
    {
            List<CellIndex> randomCells = new List<CellIndex>();
            int i = -1;
            int j = -1;
            CellIndex result = new CellIndex(i, j);

            for (int k = 0; k < cellsAmount; k++)
            {
                do
                {
                    i = Random.Range(0, rowsAmount);
                    j = Random.Range(0, ColumnsAmount);
                    result.SetValues(i,j);
                } while (randomCells.Contains(result));
                
                randomCells.Add(result);
            }

            return randomCells;
    }
    
    private Barrier GetRandomObstacle(int maxLevelObstacle)
    {
        return _obstacles[GenerateRandomIndex(maxLevelObstacle)]; 
    }

    private Barrier GetRandomTrap(int maxLevelTrap) 
    {
        return _traps[GenerateRandomIndex(maxLevelTrap)];
    }
    
    private int GenerateRandomIndex(int maxLevel)
    {
        return Random.Range(0, maxLevel);
    }

    private void GetNewRandomIndex(ref int i, ref int j, List<CellIndex> randomCellsIndexes)
    {
        CellIndex index = randomCellsIndexes[0];
        randomCellsIndexes.RemoveAt(0);
        i = index.RowIndex;
        j = index.ColumnIndex;
    }
}
    