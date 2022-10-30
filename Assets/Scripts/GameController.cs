using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int enemiesPerRound;
    [SerializeField] private float spawnTime;
    [SerializeField] private Spawner spawner;
    private int currentEnemiesCount = 0;
    private bool isSpawning = false;

    private void Update()
    {
        if (isSpawning == false)
        {
            spawner.GenerateIndexOfSpawnPosition();
            StartCoroutine(SpawnEnemies());
            isSpawning = true;
        }
        else if (isSpawning && currentEnemiesCount == enemiesPerRound)
        {
            StopCoroutine(SpawnEnemies());
            currentEnemiesCount = 0;
            isSpawning = false;
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (currentEnemiesCount < enemiesPerRound)
        {
            spawner.Spawn();
            currentEnemiesCount++;
            yield return new WaitForSeconds(spawnTime);
        }
    }
}