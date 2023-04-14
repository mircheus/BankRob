using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofsFxPool : FXPool
{
    public void SubscribeFxPool(List<Roof> roofs)
    {
        foreach (var obstacle in roofs)
        {
            obstacle.Destroyed += EnableFX;
        }
    }
    
    public void UnsubscribeFxPool(List<Roof> roofs)
    {
        foreach (var obstacle in roofs)
        {
            obstacle.Destroyed -= EnableFX;
        }
    }
}
