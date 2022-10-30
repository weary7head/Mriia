using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Ammo ammoPrefab;
    [SerializeField] protected Transform spawnPosition;
    [SerializeField] protected float fireRate = 1.5f;
    [SerializeField] protected int generalBulletsCount = 30;
    
    protected float nextTimeToFire = 0f;
    protected Queue<Ammo> bullets;
    protected Animal animal;
    
    protected IEnumerator EnqueueAmmo(Ammo ammo, float seconds = 3f)
    {
        yield return new WaitForSeconds(seconds);
        ammo.gameObject.SetActive(false);
        bullets.Enqueue(ammo);
    }
    
    protected void InitializeBullets()
    {
        for (int i = 0; i < generalBulletsCount; i++)
        {
            Ammo bottle = Instantiate(ammoPrefab);
            bottle.gameObject.SetActive(false);
            bullets.Enqueue(bottle);
        }
    }

    public void SetTarget(Animal animal)
    {
        this.animal = animal;
    }

    public abstract void Shoot(Vector2 direction);
}