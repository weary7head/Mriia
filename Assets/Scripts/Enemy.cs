using System;
using Player.Input;
using UnityEngine;

public class Enemy : Animal
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private LayerMask heroMask;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float attackRange = 10;
    
    public override event Action<Animal> OnDie;
    
    private Vector2 direction;
    private Animal hero;
    private AnimationState previouslyState;

    private void Start()
    {
        Collider2D c = Physics2D.OverlapCircle(targetTransform.position, 100f, heroMask);
        hero = c.GetComponent<Hero>();
        SetState(AnimationState.Walk);
    }

    private void Update()
    {
        direction = targetTransform.position - hero.transform.position;
        spriteRenderer.flipX = direction.x < 0;
        if (direction.sqrMagnitude <= attackRange)
        {
            SetState(AnimationState.Fire);
            Attack();
        }
        else
        {
            direction = -direction;
            direction.y = 0;
            targetTransform.Translate(direction * (speed * Time.deltaTime));
            SetState(AnimationState.Walk);
        }
    }

    public override void Attack()
    {
        //weapon.Shoot(-direction);
    }

    public override void GetDamage(float damage)
    {
        OnDie?.Invoke(this);
        throw new System.NotImplementedException();
    }
    
    private void SetState(AnimationState state)
    {
        if (state == previouslyState)
        {
            return;
        }
        previouslyState = state;
        switch (state)
        {
            case AnimationState.Walk:
                animator.SetBool("Attack", false);
                animator.SetFloat("Move", 1f);
                break;
            case AnimationState.Fire:
                animator.SetBool("Attack", true);
                animator.SetFloat("Move", 0f);
                break;
        }
    }
}