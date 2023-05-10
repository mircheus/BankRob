using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedPerk : Perk
{
    [SerializeField] private RobberMovement _robberMovement;
    [SerializeField] private ObstacleCrusher _obstacleCrusher;
    [SerializeField] private float _actionTime;
    [SerializeField] private float _increasedSpeed;
    [SerializeField] private int _increasedDamage;

    public void Activate()
    {
        _robberMovement.SetIncreasedSpeedBySpeedPerk(this, _increasedSpeed);
        _obstacleCrusher.IncreaseDamageBySpeedPerk(this, _increasedDamage);
        StartCoroutine(DeactivateAfterExecution());
    }

    private IEnumerator DeactivateAfterExecution()
    {
        yield return new WaitForSeconds(_actionTime);
        DeactivatePerk();
    }

    private void DeactivatePerk()
    {
        _robberMovement.SetDefaultSpeedByPerk(this);
        _obstacleCrusher.SetDefaultDamageBySpeedPerk(this);
    }
}
