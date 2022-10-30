using UnityEngine;

public abstract class Ammo : MonoBehaviour
{
    [SerializeField] protected Transform ammoTransform;
    [SerializeField] protected float speed = 10.0f;
    [SerializeField] protected float damage = 10f;

    protected Vector3 direction;
    protected Animal target;

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void SetTarget(Animal animal)
    {
        target = animal;
    }
}