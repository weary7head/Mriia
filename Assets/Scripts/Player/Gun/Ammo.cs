using UnityEngine;

public abstract class Ammo : MonoBehaviour
{
    [SerializeField] protected Transform ammoTransform;
    [SerializeField] protected float speed = 10.0f;
    [SerializeField] protected float damage = 10f;

    protected Vector3 direction;

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
}