using UnityEngine;

namespace Player.Input
{
    public class Hero : Animal
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Gun.Gun gun;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform targetTransform;
        private PlayerInputAction playerInputAction;
        private Vector2 direction;
        private Vector2 fireDirection;
        private HeroAnimationState previouslyState;
        private bool reloading = false;

        private void Awake()
        {
            playerInputAction = new PlayerInputAction();
        }

        private void OnEnable()
        {
            playerInputAction.Enable();
            gun.OnStartReload += StartReload;
            gun.OnEndReload += EndReload;
        }

        private void Start()
        {
            SetState(HeroAnimationState.Idle);
            fireDirection = targetTransform.right;
        }

        private void Update()
        {
            if (playerInputAction.Player.Fire.IsPressed() == false && reloading == false)
            {
                Move();
            }

            if (reloading == false)
            {
                Attack();
            }
        }

        private void OnDisable()
        {
            playerInputAction.Disable();
            gun.OnStartReload -= StartReload;
            gun.OnEndReload -= EndReload;
        }

        private void Move()
        {
            direction = playerInputAction.Player.Move.ReadValue<Vector2>();
            if (direction != Vector2.zero)
            {
                SetState(HeroAnimationState.Walk);
                fireDirection = direction;
                spriteRenderer.flipX = direction != Vector2.right;
                targetTransform.Translate(direction * (speed * Time.deltaTime));
            }
            else
            {
                SetState(HeroAnimationState.Idle);
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
                    animator.SetBool("Reload", false);
                    break;
                case HeroAnimationState.Reload:
                    animator.SetBool("Reload", true);
                    animator.SetBool("Attack", false);
                    break;
            }
        }

        private void StartReload()
        {
            reloading = true;
            SetState(HeroAnimationState.Reload);
        }

        private void EndReload()
        {
            reloading = false;
            SetState(HeroAnimationState.Fire);
        }

        public override void Attack()
        {
            if (playerInputAction.Player.Fire.IsPressed())
            {
                SetState(HeroAnimationState.Fire);
                gun.Shoot(fireDirection);
            }
        }

        public override void GetDamage(float damage)
        {
            health = Mathf.Clamp(health - damage, 0, 100);
        }
    }
}