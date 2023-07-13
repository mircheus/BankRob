using UnityEngine;

public class DashPerk : Perk
{
    [Header("Unique")]
    [SerializeField] private RobberMovement _robberMovement;
    [SerializeField] private ObstacleCrusher _obstacleCrusher;
    [SerializeField] private int _increasedDamage;

    public override void Activate()
    {
        base.Activate();
        _robberMovement.SetIncreasedSpeedBy(this);
        _obstacleCrusher.IncreaseDamageBySpeedPerk(this, _increasedDamage);
            
        if (_obstacleCrusher.ObstacleToCrush != null)
        {
            _obstacleCrusher.ObstacleToCrush.ApplyDamage(_increasedDamage);
            _robberMovement.SetIncreasedSpeedBy(this);
        }
    }

    protected override void Deactivate()
    {
        _robberMovement.SetDefaultSpeedBy(this);
        _obstacleCrusher.SetDefaultDamageBySpeedPerk(this);
        gameObject.SetActive(false);
    }
}
