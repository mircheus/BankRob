using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Perk
{
    [Header("Unique")]
    [SerializeField] private RobberMovement _robberMovement;
    
    public override void Activate()
    {
        base.Activate();
        _robberMovement.SetShieldActive(true);
    }
    
    protected override void Deactivate()
    {
        _robberMovement.SetShieldActive(false);
        base.Deactivate();
    }
}
