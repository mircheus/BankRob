using UnityEngine;

public class AnimationSwitcher : MonoBehaviour
{
   [SerializeField] private WallCrusher _wallCrusher;
   
   private Animator _animator;
   private int _attack = Animator.StringToHash("Attack");
   private int _attackingWall = Animator.StringToHash("AttackingWall");
   
   
   private void OnEnable()
   {
      // _wallCrusher.WallCollided += PlayAttackAnimation;
      _wallCrusher.WallCollided += SwitchToAttackState;
      _wallCrusher.WallDestroyed += SwitchToFallState;
   }

   private void OnDisable()
   {
      // _wallCrusher.WallCollided -= PlayAttackAnimation;
      _wallCrusher.WallCollided -= SwitchToAttackState;
      _wallCrusher.WallDestroyed -= SwitchToFallState;
   }

   private void Start()
   {
      _animator = GetComponent<Animator>();
   }

   public void PlayAttackAnimation()
   {
      _animator.Play(_attack);
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
