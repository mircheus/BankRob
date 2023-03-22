using System;
using System.Collections.Generic;
using UnityEngine;

public class Robber : MonoBehaviour
{
    [SerializeField] private KeyCollector _keyCollector;
    
    private List<Color> _levelColors = new List<Color>() { Color.yellow , Color.green, Color.blue};
    private Color[] _colors = new[] { Color.yellow, Color.green, Color.blue };
    private int _level = 0;
    private Material _material;
    private RobberMovement _robberMovement;
    private WallCrusher _wallCrusher;
    private AnimationSwitcher _animationSwitcher;
    
    public int Level => _level;

    private void OnEnable()
    {
        _keyCollector.KeyCollected += OnKeyCollected;
    }

    private void OnDisable()
    {
        _keyCollector.KeyCollected -= OnKeyCollected;
    }

    private void Start()
    {
        // _material = GetComponent<MeshRenderer>().material;
        // _material.color = _levelColors[_level];
        _material = GetComponentInChildren<SkinnedMeshRenderer>().material;
        _robberMovement = GetComponent<RobberMovement>();
        _keyCollector = GetComponent<KeyCollector>();
        _robberMovement.enabled = false;
        _wallCrusher = GetComponent<WallCrusher>();
        _animationSwitcher = GetComponent<AnimationSwitcher>();
    }
    
    public void UpgradeLevel()
    {
        if (_level + 1 < _levelColors.Count)
        {
            _level++;
            // _material.color = _levelColors[_level];
            _wallCrusher.IncreaseDamage(_level);
        }
    }

    public void ActivateMovement()
    {
        _robberMovement.enabled = true;
        _animationSwitcher.PlayAttackAnimation();
    }

    private void OnKeyCollected()
    {
        Debug.Log("KeyCollected");
    }
}
