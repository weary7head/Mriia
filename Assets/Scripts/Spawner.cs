using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPositions;
    [SerializeField] private List<Animal> prefabs;
    public event Action AllEnemiesDied;
    private List<Animal> animals = new List<Animal>();
    private int index = 0;
    
    public void GenerateIndexOfSpawnPosition()
    {
        index = Random.Range(0, spawnPositions.Count);
    }
    
    public void Spawn()
    {
        Animal animal = Instantiate(prefabs[Random.Range(0, prefabs.Count)], spawnPositions[index].position, Quaternion.identity);
        animal.OnDie += EnemyDie;
        animals.Add(animal);
    }

    private void EnemyDie(Animal animal)
    {
        animals.Remove(animal);
        Destroy(animal.gameObject);
        if (animals.Count == 0)
        {
            AllEnemiesDied?.Invoke();
        }
    }
}