using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robber : MonoBehaviour
{
    private List<Color> _levelColors = new List<Color>() { Color.yellow , Color.green, Color.blue};
    private int _level = 0;
    private Material _material;

    public int Level => _level;

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _material.color = _levelColors[_level];
    }
    
    public void UpgradeLevel()
    {
        _level++;
        _material.color = _levelColors[_level];
    }
}
