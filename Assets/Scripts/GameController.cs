using System.Collections;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject[] mriiaSprites; 
    [SerializeField] private int enemiesPerRound;
    [SerializeField] private float spawnTime;
    [SerializeField] private Spawner spawner;
    private int currentEnemiesCount = 0;
    private bool isSpawning = false;
    private int currentWave = 0;
    private bool allKilled = true;
    private int currentSprite = 0;
    private bool waveComing = false;

    private void OnEnable()
    {
        spawner.AllEnemiesDied += EnemiesDied;
    }

    private void OnDisable()
    {
        spawner.AllEnemiesDied -= EnemiesDied;
    }

    private void Update()
    {
        if (currentWave < 2 && allKilled && waveComing == false)
        {
            Debug.Log("HERE");
            mriiaSprites[currentSprite].SetActive(true);
            StartCoroutine(HideText());
            allKilled = false;
            waveComing = true;
            spawner.GenerateIndexOfSpawnPosition();
            StartCoroutine(SpawnEnemies());
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

    private IEnumerator HideText()
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        text.gameObject.SetActive(false);
    }

    private void EnemiesDied()
    {
        currentWave++;
        allKilled = true;
        mriiaSprites[currentSprite].SetActive(false);
        currentSprite++;
        waveComing = false;
        currentEnemiesCount = 0;
    }
}