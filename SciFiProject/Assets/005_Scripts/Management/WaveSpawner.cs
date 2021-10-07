using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public int baseCount { get; set; }
        public List<Transform> baseSpawnsPoints { get; set; }

        public string name;
        public Transform enemy;
        public List<Transform> spawnPoint = new List<Transform>();
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float timeBetweenIncreaseEnemies = 2f;
    private float waveCountdown;

    public SpawnState state = SpawnState.COUNTING;

    private void Awake()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].baseSpawnsPoints = new List<Transform>();
        }
    }

    private void Start()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].baseCount = waves[i].count;
            waves[i].baseSpawnsPoints.Clear();
            for (int j = 0; j < waves[i].spawnPoint.Count; j++)
            {
                waves[i].baseSpawnsPoints.Add(waves[i].spawnPoint[j]);
            }

        }

        waveCountdown = timeBetweenWaves;

        StartCoroutine(TimerBetweenIncreaseEnemies());
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {

            WaveCompleted();
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                // Start Wave Spawn
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All waves complete");
        }
        else
        {
            nextWave++;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave :" + _wave.name);
        state = SpawnState.SPAWNING;

        // Spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy, _wave.spawnPoint[i]);
            yield return new WaitForSeconds(_wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy, Transform _spawnPoint)
    {
        // Spawn Enemy
        Transform enemy = Instantiate(_enemy, _spawnPoint.position, _spawnPoint.rotation);
        enemy.SetParent(_spawnPoint);
        enemy.localPosition = Vector3.zero;


        Debug.Log("Spawning Enemy :" + _enemy.name);
    }

    private IEnumerator TimerBetweenIncreaseEnemies()
    {
        yield return new WaitForSeconds(timeBetweenIncreaseEnemies);

        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].count += 1;
            waves[i].spawnPoint.Add(waves[i].spawnPoint[waves[i].spawnPoint.Count - 1]);
        }

        StartCoroutine(TimerBetweenIncreaseEnemies());
    }

    public void ReinitWaves()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].count = waves[i].baseCount;
            waves[i].spawnPoint.Clear();
            for (int j = 0; j < waves[i].baseSpawnsPoints.Count; j++)
            {
                waves[i].spawnPoint.Add(waves[i].baseSpawnsPoints[j]);
            }
            
        }
    }
}
