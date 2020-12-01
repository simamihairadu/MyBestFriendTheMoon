using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING,WAITING,COUNTING};
    [System.Serializable]
    public class Wave
    {
        public float waveLength;
        public bool spawnAsteroids;
        public bool spawnRanged;
        public bool spawnFollow;
        public GameObject asteroidSpawnerUp;
        public GameObject asteroidSpawnerDown;
        public GameObject rangedAlienSpawner;
        public GameObject followAlienSpawner;
    }

    public Wave[] waves;
    public GameObject boss;
    public GameObject uiController;
    private int nextWave = 0;
    private bool startBoss = false;
    public float timeBetweenWaves = 5f;
    public float waveCountdown;
    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if(startBoss)
        {
            StartBossFight();
        }
        if(state == SpawnState.WAITING)
        {
            if(!EnemyIsAlive())
            {
                GoToNextWave();
            }
            else
            {
                return;
            }
        }
        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    void GoToNextWave()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if(nextWave + 1 == waves.Length)
        {
            startBoss = true;
            return;
        }
        nextWave++;
    }
    void StartBossFight()
    {
        boss.SetActive(true);
        uiController.GetComponent<UIController>().bossHpSlider.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;
        while (wave.waveLength > 0)
        {
            if (wave.spawnAsteroids)
            {
                SpawnAsteroids(wave);
            }
            if (wave.spawnFollow)
            {
                SpawnFollowAlien(wave);
            }
            if (wave.spawnRanged)
            {
                SpawnRangedAlien(wave);
            }
            yield return new WaitForSeconds(1f);
            wave.waveLength -= 1f;
        }
        state = SpawnState.WAITING;
        yield break;
    }
    private void SpawnRangedAlien(Wave wave)
    {
        wave.rangedAlienSpawner.GetComponent<EnemySpawner>().SpawnX();
        wave.rangedAlienSpawner.GetComponent<EnemySpawner>().SpawnY();
    }

    private void SpawnFollowAlien(Wave wave)
    {
        wave.followAlienSpawner.GetComponent<EnemySpawner>().SpawnX();
        wave.followAlienSpawner.GetComponent<EnemySpawner>().SpawnY();
    }

    private void SpawnAsteroids(Wave wave)
    {
        wave.asteroidSpawnerUp.GetComponent<EnemySpawner>().SpawnX();
        wave.asteroidSpawnerUp.GetComponent<EnemySpawner>().SpawnY();
        wave.asteroidSpawnerDown.GetComponent<EnemySpawner>().SpawnX();
        wave.asteroidSpawnerDown.GetComponent<EnemySpawner>().SpawnX();
    }
}
