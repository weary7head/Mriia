using UnityEngine;

public class Bullet : Ammo
{
    private void Update()
    {
        direction *= speed;
        direction = Vector3.ClampMagnitude(direction, speed);
        ammoTransform.Translate(Time.deltaTime * direction, Space.World);
    }

    private void OnCollisionEnter(Collision other)
    {
       
    }
}