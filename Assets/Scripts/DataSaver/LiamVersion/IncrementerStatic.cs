using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementerStatic : MonoBehaviour
{
    public void IncrementMoney()
    {
        StaticStats.IncrementMoney();
    }

    public void IncrementKeys()
    {
        StaticStats.IncrementKeys();
    }
}
