using UnityEngine;

namespace Player.Input
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Gun.Gun gun;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform targetTransform;
        [SerializeField] private float speed;
        private PlayerInputAction playerInputAction;
        private Vector2 direction;
        private Vector2 fireDirection;
        private HeroAnimationState previouslyState;

        private void Awake()
        {
            playerInputAction = new PlayerInputAction();
        }

        private void OnEnable()
        {
            playerInputAction.Enable();
        }

        private void Start()
        {
            SetState(HeroAnimationState.Idle);
            fireDirection = targetTransform.right;
        }

        private void Update()
        {
            if ( playerInputAction.Player.Fire.IsPressed() == false)
            {
                Move();
            }
            Fire();
        }

        private void OnDisable()
        {
            playerInputAction.Disable();
        }

        private void Move()
        {
            direction = playerInputAction.Player.Move.ReadValue<Vector2>();
            if (direction != Vector2.zero)
            {
                SetState(HeroAnimationState.Walk);
                fireDirection = direction;
                spriteRenderer.flipX = direction != Vector2.right;
                targetTransform.Translate(direction * speed * Time.deltaTime);
            }
            else
            {
                SetState(HeroAnimationState.Idle);
            }
        }

        private void Fire()
        {
            if (playerInputAction.Player.Fire.IsPressed())
            {
                SetState(HeroAnimationState.Fire);
                gun.Shoot(fireDirection);
            }
        }
        
        private void SetState(HeroAnimationState state)
        {
            if (state == previouslyState)
            {
                return;
            }
            previouslyState = state;
            switch (state)
            {
                case HeroAnimationState.Idle:
                    animator.SetBool("Attack", false);
                    animator.SetFloat("Move", 0f);
                    break;
                case HeroAnimationState.Walk:
                    animator.SetBool("Attack", false);
                    animator.SetFloat("Move", 1f);
                    break;
                case HeroAnimationState.Fire:
                    animator.SetBool("Attack", true);
                    animator.SetFloat("Move", 0f);
                    break;
            }
        }
    }
}