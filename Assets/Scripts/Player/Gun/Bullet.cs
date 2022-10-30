using UnityEngine;

public class Bullet : Ammo
{
    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        direction *= speed;
        direction = Vector3.ClampMagnitude(direction, speed);
        ammoTransform.Translate(Time.deltaTime * direction, Space.World);
        if (target == null)
        {
            return;
        }
        if ((target.transform.position - ammoTransform.position).sqrMagnitude <= 3)
        {
            Debug.Log("TRUE");
            target.GetDamage(damage);
            Destroy(gameObject);
        }
    }
}