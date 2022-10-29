using System.Collections.Generic;
using UnityEngine;

public class BottleThrower : Weapon
{
    private void Start()
    {
        bullets = new Queue<Ammo>(generalBulletsCount);
        InitializeBullets();
    }
    
    public override void Shoot(Vector2 direction)
    {
        if (Time.time >= nextTimeToFire)
        {
            spawnPosition.localPosition = direction.normalized == Vector2.left ? new Vector3(-2.3f,1.2f,0) : new Vector3(2.3f,1.2f,0);
            nextTimeToFire = Time.time + 1f / fireRate;
            Ammo bottle = bullets.Dequeue();
            bottle.transform.position = spawnPosition.position;
            bottle.gameObject.SetActive(true);
            bottle.SetDirection(direction);
            StartCoroutine(EnqueueAmmo(bottle));
        }
    }
}
