using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticStats
{
    private static int _money;
    private static int _keys;

    public static int Money => _money;
    public static int Keys => _keys;
    
    public static void IncrementKeys()
    {
        _keys++;
    }

    public static void IncrementMoney()
    {
        _money += 100;
    }
}
