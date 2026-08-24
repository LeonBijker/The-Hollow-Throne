using UnityEngine;

public class RoomEnemySpawner : MonoBehaviour
{
    [Header("Normal Enemies")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Mini Boss")]
    [SerializeField] private GameObject miniBossPrefab;
    [SerializeField, Range(0f, 1f)] private float miniBossChance = 0.2f;

    [Header("Enemy Amount")]
    [SerializeField] private int minEnemies = 3;
    [SerializeField] private int maxEnemies = 8;

    [Header("Spawn Area")]
    [SerializeField] private Transform[] spawnPoints;

    private RoomEnemyTracker roomEnemyTracker;

    private void Awake()
    {
        roomEnemyTracker = GetComponent<RoomEnemyTracker>();

        if (roomEnemyTracker == null)
        {
            Debug.LogError("RoomEnemyTracker is missing from this room.", this);
        }
    }

    private void Start()
    {
        SpawnRoom();
    }

    private void SpawnRoom()
    {
        if (roomEnemyTracker == null)
            return;

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned. Room will be considered clear.", this);
            roomEnemyTracker.SpawningFinished();
            return;
        }

        if (Random.value <= miniBossChance && miniBossPrefab != null)
        {
            SpawnMiniBoss();
        }
        else
        {
            int enemyAmount = Random.Range(minEnemies, maxEnemies + 1);

            for (int i = 0; i < enemyAmount; i++)
            {
                SpawnRandomEnemy();
            }
        }

        roomEnemyTracker.SpawningFinished();
    }

    private void SpawnRandomEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("No normal enemy prefabs assigned.", this);
            return;
        }

        GameObject enemyPrefab =
            enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Transform spawnPoint =
            spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = Instantiate(
            enemyPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        Health enemyHealth = enemy.GetComponent<Health>();

        if (enemyHealth != null)
        {
            roomEnemyTracker.RegisterEnemy(enemyHealth);
        }
        else
        {
            Debug.LogWarning(
                $"Spawned enemy '{enemy.name}' has no Health component.",
                enemy
            );
        }
    }

    private void SpawnMiniBoss()
    {
        if (miniBossPrefab == null)
        {
            Debug.LogWarning("Mini boss prefab is not assigned.", this);
            return;
        }

        Transform spawnPoint =
            spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject miniBoss = Instantiate(
            miniBossPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        Health miniBossHealth = miniBoss.GetComponent<Health>();

        if (miniBossHealth != null)
        {
            roomEnemyTracker.RegisterEnemy(miniBossHealth);
        }
        else
        {
            Debug.LogWarning(
                $"Mini boss '{miniBoss.name}' has no Health component.",
                miniBoss
            );
        }
    }
}