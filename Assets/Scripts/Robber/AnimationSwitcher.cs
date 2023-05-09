using UnityEngine;

public class AnimationSwitcher : MonoBehaviour
{
   [SerializeField] private ObstacleCrusher obstacleCrusher;
   
   private Animator _animator;
   // private int _attack = Animator.StringToHash("Attack");
   private int _attack = Animator.StringToHash("axe_attack_overdrive");
   private int _attackingWall = Animator.StringToHash("AttackingWall");
   private int _firstAttack = Animator.StringToHash("first_attack");
   
   private void OnEnable()
   {
      // _wallCrusher.WallCollided += PlayAttackAnimation;
      obstacleCrusher.ObstacleCollided += SwitchToAttackState;
      obstacleCrusher.ObstacleDestroyed += SwitchToFallState;
   }

   private void OnDisable()
   {
      // _wallCrusher.WallCollided -= PlayAttackAnimation;
      obstacleCrusher.ObstacleCollided -= SwitchToAttackState;
      obstacleCrusher.ObstacleDestroyed -= SwitchToFallState;
   }

   private void Start()
   {
      _animator = GetComponent<Animator>();
   }

   public void PlayAttackAnimation()
   {
      _animator.Play(_attack);
   }

   public void PlayFirstAttack()
   {
      _animator.Play(_firstAttack);
   }

   private void SwitchToAttackState()
   {
      _animator.SetBool(_attackingWall, true);
   }

   private void SwitchToFallState()
   {
      _animator.SetBool(_attackingWall, false);
   }
}
