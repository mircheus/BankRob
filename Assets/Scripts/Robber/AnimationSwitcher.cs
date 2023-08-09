using UnityEngine;

public class AnimationSwitcher : MonoBehaviour
{
   [SerializeField] private ObstacleCrusher _obstacleCrusher;
   [SerializeField] private RobberMovement _robberMovement;
   [SerializeField] private Animator _animator;
   
   private int _attack = Animator.StringToHash("axe_attack_overdrive");
   private int _attackingWall = Animator.StringToHash("AttackingWall");
   private int _firstAttackJump = Animator.StringToHash("first_attack_jump");
   private int _died = Animator.StringToHash("Died");
   private int _run = Animator.StringToHash("Run");
   
   private void OnEnable()
   {
      _obstacleCrusher.ObstacleCollided += SwitchToAttackState;
      _obstacleCrusher.ObstacleDestroyed += SwitchToFallState;
      _robberMovement.GetStopped += SwitchToDieState;
      _robberMovement.ReachedVault += PlayRun;
   }

   private void OnDisable()
   {
      _obstacleCrusher.ObstacleCollided -= SwitchToAttackState;
      _obstacleCrusher.ObstacleDestroyed -= SwitchToFallState;
      _robberMovement.GetStopped -= SwitchToDieState;
      _robberMovement.ReachedVault -= PlayRun;
   }

   public void PlayAttackAnimation()
   {
      _animator.Play(_attack);
   }

   public void PlayFirstAttack()
   {
      _animator.Play(_firstAttackJump);
   }

   private void SwitchToAttackState()
   {
      _animator.SetBool(_attackingWall, true);
   }

   private void SwitchToFallState()
   {
      _animator.SetBool(_attackingWall, false);
   }

   private void SwitchToDieState()
   {
      _animator.SetTrigger(_died);
   }

   private void PlayRun()
   {
      _animator.Play(_run);
   }
}
