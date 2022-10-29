using UnityEngine;

public class Bottle : Ammo
{
    private void Update()
    {
        direction *= speed;
        direction = Vector3.ClampMagnitude(direction, speed);
        ammoTransform.Translate(Time.deltaTime * direction, Space.World);
    }
}