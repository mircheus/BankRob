using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChanger : MonoBehaviour
{
    private const float SlowmoScale = .2f;
    
    [SerializeField] private Robbery _robbery;
    [SerializeField] private float _timeScale;
    
    private void OnEnable()
    {
        _robbery.BankRobbed += OnBankRobbed;
        _robbery.BankNotRobbed += OnBankNotRobbed;
    }

    private void OnDisable()
    {
        _robbery.BankRobbed -= OnBankRobbed;
        _robbery.BankNotRobbed -= OnBankNotRobbed;
    }

    private void Start()
    {
        Time.timeScale = _timeScale;
    }

    public void EnableSlowmo()
    {
        Time.timeScale = SlowmoScale;
    }
    
    public void DisableSlowmo()
    {
        Time.timeScale = 1;
    }

    public void StopTime()
    {
        Time.timeScale = 0f;
    }

    private void OnBankRobbed()
    {
        SlowdownTime();
    }

    private void OnBankNotRobbed()
    {
        SlowdownTime();
    }

    private void SlowdownTime()
    {
        Time.timeScale = SlowmoScale;
    }
}
