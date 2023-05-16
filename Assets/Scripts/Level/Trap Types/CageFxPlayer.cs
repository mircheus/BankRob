using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageFxPlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _crackFx;

    public void PlayCrackFx()
    {
        _crackFx.Play();
    }
}
