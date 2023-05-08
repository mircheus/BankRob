using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Robber : MonoBehaviour
{
    [SerializeField] private KeyCollector _keyCollector;
    [SerializeField] private SkinnedMeshRenderer _bodyMesh;
    [SerializeField] private SkinnedMeshRenderer _sweaterMesh;
    [SerializeField] private SkinnedMeshRenderer _shoesMesh;
    [SerializeField] private GameObject _axe;
    [SerializeField] private ExplosionPerk _explosionPerk;
    [SerializeField] private Color[] _levelColors = new [] { Color.yellow , Color.green, Color.blue, Color.magenta, Color.red, Color.white, };
    
    private int _level = 1;
    [SerializeField] private int _columnIndex = -1;
    
    private Material _bodyMaterial;
    private Material _sweaterMaterial;
    private Material _shoesMaterial;
    private RobberMovement _robberMovement;
    private ObstacleCrusher _obstacleCrusher;
    private AnimationSwitcher _animationSwitcher;
    
    public int Level => _level;
    public int ColumnIndex => _columnIndex;

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
        _bodyMaterial = _bodyMesh.material;
        _sweaterMaterial = _sweaterMesh.material;
        _shoesMaterial = _shoesMesh.material;
        _robberMovement = GetComponent<RobberMovement>();
        _keyCollector = GetComponent<KeyCollector>();
        _robberMovement.enabled = false;
        _obstacleCrusher = GetComponent<ObstacleCrusher>();
        _animationSwitcher = GetComponent<AnimationSwitcher>();
        SetColor(_level);
    }
    
    public void UpgradeLevel()
    {
        if (_level < _levelColors.Length)
        {
            _level++;
            SetColor(_level);
            _obstacleCrusher.IncreaseDamage(_level);
        }
    }

    public void ActivateMovement()
    {
        _robberMovement.enabled = true;
        _axe.SetActive(true);
        _animationSwitcher.PlayAttackAnimation();
    }

    public void ActivatePerk()
    {
        _explosionPerk.Activate();
    }

    public void SetColumnIndex(int index)
    {
        _columnIndex = index;
    }

    private void SetColor(int level)
    {
        if (level < _levelColors.Length)
        {
            Color color = _levelColors[level - 1];
            _bodyMaterial.color = color;
            _sweaterMaterial.color = color;
            _shoesMaterial.color = color;
        }
    }

    private void OnKeyCollected()
    {
        Debug.Log("KeyCollected");
    }
}
