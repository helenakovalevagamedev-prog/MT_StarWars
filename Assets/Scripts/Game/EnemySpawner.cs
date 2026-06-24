using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const int enemyPoolCapacityValue = 20;
    private const int enemyPoolMaxSizeValue = 50;

    private float spawnDelayMin;
    private float spawnDelayMax;
    private Coroutine spawnCoroutine;
    private float timer;
    private bool isSpawning;
    private List<GameObject> enemies;
    private EnemiesConfig config;
    private SimplePool enemyPool;
    private ScreenInfo screenInfo;

    public void Init(EnemiesConfig config, ScreenInfo screenInfo)
    {
        this.config = config;
        enemyPool = new SimplePool(config.GetEnemyPrefab(), enemyPoolCapacityValue, enemyPoolMaxSizeValue);
        this.screenInfo = screenInfo;
        enemies = new List<GameObject>();
        spawnDelayMin = config.SpawnDelayMin;
        spawnDelayMax = config.SpawnDelayMax;
        isSpawning = false;
    }

    public void StartSpawning(IReadOnlyList<EnemyTypes> possibleEnemies)
    {
        isSpawning = true;
        if (spawnCoroutine != null)
        {
            return;
        }

        spawnCoroutine = StartCoroutine(SpawnLoop(possibleEnemies));
    }

    public void StopSpawning()
    {
        isSpawning = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
        foreach (GameObject enemy in enemies)
        { 
            enemyPool.Release(enemy); 
        }
        enemies.Clear();
    }

    private IEnumerator SpawnLoop(IReadOnlyList<EnemyTypes> possibleEnemies)
    {
        while (isSpawning)
        {
            var randomEnemy = Random.Range(0, possibleEnemies.Count);
            SpawnEnemy(config.GetEnemyConfigByType(possibleEnemies[randomEnemy]));
            yield return new WaitForSeconds(Random.Range(spawnDelayMin, spawnDelayMax));
        }
    }

    private void SpawnEnemy(EnemyConfig config)
    {
        Vector3 pos = new Vector3(Random.Range(-screenInfo.HalfWidth, screenInfo.HalfWidth), screenInfo.HalfHeight + 1f, 0);
        var enemyGO = enemyPool.Get();
        enemyGO.transform.position = pos;
        enemyGO.GetComponent<Enemy>().Init(config.Cost, config.Speed, config.Sprite, enemyPool.Release, screenInfo);
        enemies.Add(enemyGO);
    }
}