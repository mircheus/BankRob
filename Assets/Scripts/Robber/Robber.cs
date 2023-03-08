using System;
using System.Collections.Generic;
using UnityEngine;

public class Robber : MonoBehaviour
{
    private List<Color> _levelColors = new List<Color>() { Color.yellow , Color.green, Color.blue};
    private int _level = 0;
    private Material _material;
    private RobberMovement _robberMovement;
    private WallCrusher _wallCrusher;
    
    public int Level => _level;

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _material.color = _levelColors[_level];
        _robberMovement = GetComponent<RobberMovement>();
        _robberMovement.enabled = false;
        _wallCrusher = GetComponent<WallCrusher>();
    }
    
    public void UpgradeLevel()
    {
        _level++;
        _material.color = _levelColors[_level];
        _wallCrusher.IncreaseDamage(_level);
    }

    public void ActivateMovement()
    {
        _robberMovement.enabled = true;
    }
}
