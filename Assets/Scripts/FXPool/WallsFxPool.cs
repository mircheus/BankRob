using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsFxPool : FXPool
{
    public void SubscribeFxPool(List<Obstacle> obstacles)
    {
        foreach (var obstacle in obstacles)
        {
            // obstacle.Destroyed += EnableFX;
        }
    }
    
    public void UnsubscribeFxPool(List<Obstacle> obstacles)
    {
        foreach (var obstacle in obstacles)
        {
            // obstacle.Destroyed -= EnableFX;
        }
    }
}
