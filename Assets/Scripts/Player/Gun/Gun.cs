using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Gun
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private float timeToReload = 2f;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private float fireRate = 1.5f;
        [SerializeField] private int generalBulletsCount = 30;
        [SerializeField] private int bulletsCount = 30;
        
        public event Action OnStartReload;
        public event Action OnEndReload;
        
        private float nextTimeToFire = 0f;
        private Queue<Bullet> bullets;
        private int currentBulletsCount;

        private void Start()
        {
            currentBulletsCount = bulletsCount;
            bullets = new Queue<Bullet>(generalBulletsCount);
            InitializeBullets();
        }

        public void Shoot(Vector2 direction)
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
                Bullet bullet = bullets.Dequeue();
                bullet.transform.position = spawnPosition.position;
                bullet.gameObject.SetActive(true);
                bullet.SetDirection(direction);
                currentBulletsCount--;
                StartCoroutine(EnqueueBullet(bullet));
            }
        }

        private void InitializeBullets()
        {
            for (int i = 0; i < generalBulletsCount; i++)
            {
                Bullet bullet = Instantiate(bulletPrefab);
                bullet.gameObject.SetActive(false);
                bullets.Enqueue(bullet);
            }
        }

        private IEnumerator EnqueueBullet(Bullet bullet, float seconds = 3f)
        {
            yield return new WaitForSeconds(seconds);
            bullet.gameObject.SetActive(false);
            bullets.Enqueue(bullet);
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