using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : Barrier
{
    [SerializeField] private int _health;
    [SerializeField] private GameObject[] _damagedForms;
    [SerializeField] private ParticleSystem _damageFX;
    [SerializeField] private ParticleSystem _destroyFX;
    [SerializeField] private float _destroyDelay;
    [SerializeField] private Obstacle _iceCube;

    private int _damageFormsAmount;
    private int _currentDamageForm = 0;
    private BoxCollider _boxCollider;
    private bool _isFreezed = false;
    private bool _isDestroyed = false;

    public event UnityAction Damaged;
    public event UnityAction Destroyed;

    private void OnEnable()
    {
        Destroyed += OnDestroyed;
    }

    private void OnDisable()
    {
        Destroyed -= OnDestroyed;
    }

    private void Start()
    {
        _damageFormsAmount = _damagedForms.Length;
        _currentDamageForm = 0;
        InitializeUndamagedForm();
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        NextDamageForm();
        _damageFX.Play();
        Damaged?.Invoke();
        
        if (_health <= 0)
        {
            Destroyed?.Invoke();
        }
    }

    private void OnDestroyed()
    {
        _destroyFX.Play();
        _boxCollider.isTrigger = true;
        _damagedForms[_currentDamageForm].SetActive(false);
        StartCoroutine(DisableAfterSomeTime());
        _isDestroyed = true;
    }

    private void InitializeUndamagedForm()
    {
        _damagedForms[0].SetActive(true);

        if (_damagedForms.Length == 1)
        {
            return;
        }

        for (int i = 1; i < _damagedForms.Length; i++)
        {
            _damagedForms[i].SetActive(false);
        }
    }

    private void NextDamageForm()
    {
        if (_damagedForms.Length == 1)
        {
            return; 
        }
        
        if (_currentDamageForm + 1 < _damageFormsAmount)
        {
            _damagedForms[_currentDamageForm].SetActive(false);
            _currentDamageForm++;
            _damagedForms[_currentDamageForm].SetActive(true);
        }
    }

    private IEnumerator DisableAfterSomeTime()
    {
        yield return new WaitForSeconds(_destroyDelay);
        gameObject.SetActive(false);
    }

    public void GetFreezedBy(FreezePerk freezePerk)
    {
        if (_isFreezed == false && _isDestroyed == false)
        {
            _iceCube.gameObject.SetActive(true);
            _iceCube.Destroyed += OnDestroyed;
            _isFreezed = true;
        }
    }
}
