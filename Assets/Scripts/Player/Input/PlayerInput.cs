using System;
using UnityEngine;

namespace Player.Input
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Gun.Gun gun;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform targetTransform;
        [SerializeField] private float speed;
        private PlayerInputAction playerInputAction;
        private Vector2 direction;
        private Vector2 fireDirection;

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
            fireDirection = targetTransform.right;
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
            direction = playerInputAction.Player.Move.ReadValue<Vector2>();
            if (direction != Vector2.zero)
            {
                fireDirection = direction;
                spriteRenderer.flipY = direction != Vector2.right;
                targetTransform.Translate(direction * speed * Time.deltaTime);
            }
        }

        private void Fire()
        {
            if (playerInputAction.Player.Fire.IsPressed())
            {
                gun.Shoot(fireDirection);
            }
        }
    }
}