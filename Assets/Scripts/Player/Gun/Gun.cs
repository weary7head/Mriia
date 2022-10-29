using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Gun
{
    public class Gun : Weapon
    {
        [SerializeField] private float timeToReload = 2f;
        [SerializeField] private int bulletsCount = 30;
        
        public event Action OnStartReload;
        public event Action OnEndReload;
        
        private int currentBulletsCount;

        private void Start()
        {
            currentBulletsCount = bulletsCount;
            bullets = new Queue<Ammo>(generalBulletsCount);
            InitializeBullets();
        }

        public override void Shoot(Vector2 direction)
        {
            if (currentBulletsCount == 0)
            {
                StartCoroutine(Reload());
                return;
            }
            if (Time.time >= nextTimeToFire)
            {
                spawnPosition.localPosition = direction == Vector2.left ? new Vector3(-2.4f,0.6f,0) : new Vector3(2.4f,0.6f,0);
                nextTimeToFire = Time.time + 1f / fireRate;
                Ammo bullet = bullets.Dequeue();
                bullet.transform.position = spawnPosition.position;
                bullet.gameObject.SetActive(true);
                bullet.SetDirection(direction);
                currentBulletsCount--;
                StartCoroutine(EnqueueAmmo(bullet));
            }
        }

        private IEnumerator Reload()
        {
            OnStartReload?.Invoke();
            yield return new WaitForSeconds(timeToReload);
            currentBulletsCount = bulletsCount;
            OnEndReload?.Invoke();
        }
    }
}