using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Gun
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Camera camera;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private float fireRate = 1.5f;
        [SerializeField] private int bulletsCount = 30;
        private float nextTimeToFire = 0f;
        private Queue<Bullet> bullets;

        private void Start()
        {
            bullets = new Queue<Bullet>(bulletsCount);
            InitializeBullets();
        }

        private void Shoot(Vector2 direction)
        {
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Bullet bullet = bullets.Dequeue();
                //laser.SetDirection(direction);
                StartCoroutine(EnqueueBullet(bullet));
            }
        }

        private void InitializeBullets()
        {
            for (int i = 0; i < bulletsCount; i++)
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
    }
}