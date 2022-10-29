using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float speed;

    public abstract void Attack();
    public abstract void GetDamage(float damage);
}