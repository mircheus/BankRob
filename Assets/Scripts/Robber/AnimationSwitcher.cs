using UnityEngine;

public class AnimationSwitcher : MonoBehaviour
{
   [SerializeField] private WallCrusher _wallCrusher;
   private Animator _animator;
   private int _punch = Animator.StringToHash("attack");
   
   private void OnEnable()
   {
      _wallCrusher.WallCollided += PlayAttackAnimation;
   }

   private void OnDisable()
   {
      _wallCrusher.WallCollided -= PlayAttackAnimation;
   }

   private void Start()
   {
      _animator = GetComponent<Animator>();
   }

   private void PlayAttackAnimation(Wall wall)
   {
      _animator.Play(_punch);
      Debug.Log("Attack animation");
   }
}
