using UnityEngine;

namespace Player.Input
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform targetTransform;
        [SerializeField] private float speed;
        private PlayerInputAction playerInputAction;

        private void Awake()
        {
            playerInputAction = new PlayerInputAction();
        }

        private void OnEnable()
        {
            playerInputAction.Enable();
        }

        private void Update()
        {
            Move();
            Fire();
        }

        private void OnDisable()
        {
            playerInputAction.Disable();
        }

        private void Move()
        {
            Vector2 direction = playerInputAction.Player.Move.ReadValue<Vector2>();
            if (direction != Vector2.zero)
            {
                spriteRenderer.flipY = direction != Vector2.right;
                targetTransform.Translate(direction * speed * Time.deltaTime);
            }
        }

        private void Fire()
        {
            if ( playerInputAction.Player.Fire.IsPressed())
            {
                Debug.Log("FIRE");
            }
        }
    }
}