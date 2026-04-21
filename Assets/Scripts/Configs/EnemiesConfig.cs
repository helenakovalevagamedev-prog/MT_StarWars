using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesConfig", menuName = "Scriptable Objects/EnemiesConfig")]
public class EnemiesConfig : ScriptableObject
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDelayMin;
    [SerializeField] private float spawnDelayMax;
    [SerializeField] private EnemyConfig[] enemies = new EnemyConfig[0];

    public float SpawnDelayMin => spawnDelayMin;
    public float SpawnDelayMax => spawnDelayMax;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public EnemyConfig GetEnemyConfigByType(EnemyTypes type)
    {
        return enemies.ToList().Find(enemy => enemy.Type == type);
    }
}
