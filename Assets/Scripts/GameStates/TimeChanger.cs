using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChanger : MonoBehaviour
{
    private const float SlowmoScale = .5f;
    
    [SerializeField] private Robbery _robbery;

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
        Time.timeScale = 1f;
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
