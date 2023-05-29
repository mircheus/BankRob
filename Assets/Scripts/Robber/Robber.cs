using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

public class Robber : MonoBehaviour
{
    [SerializeField] private KeyCollector _keyCollector;
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
    private AnimationSwitcher _animationSwitcher;

    public UnityAction<int> Frozen;
    
    public int Level => _level;
    public int MaxLevel => _maxLevel;
    public int ColumnIndex => _columnIndex;
    public Color[] LevelColors => _levelColors;
    public Shield Shield => _shield;

    private void OnEnable()
    {
        _keyCollector.KeyCollected += OnKeyCollected;
        GetComponent<RobberMovement>().GetStopped += OnGetStopped;
    }

    private void OnDisable()
    {
        _keyCollector.KeyCollected -= OnKeyCollected;
        GetComponent<RobberMovement>().GetStopped -= OnGetStopped;
    }

    private void Awake()
    {
        _bodyMaterial = _bodyMesh.material;
        _sweaterMaterial = _sweaterMesh.material;
        _shoesMaterial = _shoesMesh.material;
        _robberMovement = GetComponent<RobberMovement>();
        _keyCollector = GetComponent<KeyCollector>();
        _robberMovement.enabled = false;
        _obstacleCrusher = GetComponent<ObstacleCrusher>();
        _animationSwitcher = GetComponent<AnimationSwitcher>();
        _level = 0;
        SetColor(_level);
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
        // _explosionPerk.Activate();
        _perks[_level].Activate();
    }

    public void ActivatePerkTEST(int index)
    {
        _perks[index].Activate();
    }

    public void SetColumnIndex(int index)
    {
        _columnIndex = index;
    }

    public void GetFrozen()
    {
        Frozen?.Invoke(_columnIndex);
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

    private void OnKeyCollected()
    {
        Debug.Log("KeyCollected");
    }

    private void OnGetStopped()
    {
        _robberMovement.enabled = false;
    }
}
