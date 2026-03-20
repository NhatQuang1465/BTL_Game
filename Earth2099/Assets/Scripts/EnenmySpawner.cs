using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenmySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenSpawns = 2f;

    void Start()
    {
        StartCoroutine(SpawnEnenmyCoroutine());
    }
    private IEnumerator SpawnEnenmyCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];// Chọn enemy ngẫu nhiên
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Chọn spawn point ngẫu nhiên
            Instantiate(enemy, spawnPoint.position, Quaternion.identity);// Spawn enemy
        }
    }

}
