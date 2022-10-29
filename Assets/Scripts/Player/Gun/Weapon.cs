using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet bulletPrefab;
    [SerializeField] protected Transform spawnPosition;
    [SerializeField] protected float fireRate = 1.5f;
    [SerializeField] protected int generalBulletsCount = 30;
    
    protected float nextTimeToFire = 0f;
    protected Queue<Bullet> bullets;
}