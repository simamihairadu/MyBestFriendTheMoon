using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate = 1f;
    float nextSpawnX = 0.0f;
    float nextSpawnY = 0.0f;
    public float startDelay = 0.0f;

    public void SpawnX()
    {
        if (Time.timeSinceLevelLoad > nextSpawnX)
        {
            var radomSpawnTime = Random.Range(0f, 10f);
            nextSpawnX = Time.timeSinceLevelLoad + spawnRate + radomSpawnTime;
            float randomXPosition = Random.Range(transform.position.x, transform.position.x * -1);
            Vector2 spawnLocation = new Vector2(randomXPosition, transform.position.y);
            Instantiate(enemy, spawnLocation, Quaternion.identity);
        }
    }
    public void SpawnY()
    {
        if (Time.timeSinceLevelLoad > nextSpawnY)
        {
            var radomSpawnTime = Random.Range(0f, 10f);
            nextSpawnY = Time.timeSinceLevelLoad + spawnRate + radomSpawnTime;
            float randomYPosition = Random.Range(transform.position.y, transform.position.y * -1);
            Vector2 spawnLocation = new Vector2(transform.position.x, randomYPosition);
            Instantiate(enemy, spawnLocation, Quaternion.identity);
        }
    }
}
