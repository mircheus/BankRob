using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultMoneyFx : MonoBehaviour
{
    [SerializeField] private ParticleSystem _moneyParticles;
    [SerializeField] private GameObject _moneyPrefab;
    
    public void PlayFx()
    {
        _moneyParticles.Play();
        _moneyPrefab.SetActive(false);
    }
}
