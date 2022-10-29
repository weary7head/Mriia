using System;
using Player.Input;
using UnityEngine;

public class RangeEnemy : Animal
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private LayerMask heroMask;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float attackRange = 100f;
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
            targetTransform.Translate(direction * (speed * Time.deltaTime));
            SetState(AnimationState.Walk);
        }
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void GetDamage(float damage)
    {
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
    
    private Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector2.Lerp(start, end, t);

        return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));
    }
}