using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

[RequireComponent(typeof(RobberMovement))]
public class Robber : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _bodyMesh;
    [SerializeField] private SkinnedMeshRenderer _sweaterMesh;
    [SerializeField] private SkinnedMeshRenderer _shoesMesh;
    [SerializeField] private GameObject _axe;
    [SerializeField] private Shield _shield;
    [SerializeField] private Perk[] _perks;
    [SerializeField] private Color[] _levelColors = new [] { Color.yellow , Color.green, Color.blue, Color.magenta, Color.red, Color.white, };
    [SerializeField] private int _maxLevel = 4;

    [Header("Debug")]
    [SerializeField] private int _level = 0;
    [SerializeField] private int _columnIndex = -1;
    
    private Material _bodyMaterial;
    private Material _sweaterMaterial;
    private Material _shoesMaterial;
    private RobberMovement _robberMovement;
    private ObstacleCrusher _obstacleCrusher;

    public UnityAction<int> Frozen;
    public UnityAction<Robber> ReachedVault;
    
    public int Level => _level;
    public int MaxLevel => _maxLevel;
    public int ColumnIndex => _columnIndex;
    public Shield Shield => _shield;
    public RobberMovement RobberMovement => _robberMovement;

    private void Awake()
    {
        _bodyMaterial = _bodyMesh.material;
        _sweaterMaterial = _sweaterMesh.material;
        _shoesMaterial = _shoesMesh.material;
        _robberMovement = GetComponent<RobberMovement>();
        _robberMovement.enabled = false;
        _obstacleCrusher = GetComponent<ObstacleCrusher>();
        _level = 0;
        SetColor(_level);
    }
    
    private void OnEnable()
    {
        _robberMovement.GetStopped += OnGetStopped;
    }

    private void OnDisable()
    {
        _robberMovement.GetStopped -= OnGetStopped;
    }


    public int UpgradeLevel()
    {
        if (_level < _levelColors.Length)
        {
            _level++;
            SetColor(_level);
            _obstacleCrusher.IncreaseDamage(_level);
        }

        return _level;
    }

    public void SetLevel(int level)
    {
        _level = level;
    }

    public void ActivateMovement()
    {
        _robberMovement.enabled = true;
        _axe.SetActive(true);
    }

    public void ActivatePerk()
    {
        _perks[_level].Activate();
    }

    public void SetColumnIndex(int index)
    {
        _columnIndex = index;
    }

    public void GetFrozen()
    {
        if (_robberMovement.IsShieldActive == false)
        {
            Frozen?.Invoke(_columnIndex);
        }
    }

    public void InitializeAsNew()
    {
        SetLevel(0);
        SetColor(_level);
    }

    public void SetColor(int level)
    {
        if (level < _levelColors.Length)
        {
            Color color = _levelColors[level];
            _bodyMaterial.color = color;
            _sweaterMaterial.color = color;
            _shoesMaterial.color = color;
        }
    }

    private void OnGetStopped()
    {
        _robberMovement.enabled = false;
    }
}
