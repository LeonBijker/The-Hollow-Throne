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
    }

    private void Start()
    {
        SpawnRoom();
    }

    private void SpawnRoom()
    {
        if (Random.value <= miniBossChance)
        {
            SpawnMiniBoss();
            return;
        }

        int enemyAmount = Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < enemyAmount; i++)
        {
            SpawnRandomEnemy();
        }
    }

    private void SpawnRandomEnemy()
    {
        if (enemyPrefabs.Length == 0)
            return;

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
    }

    private void SpawnMiniBoss()
    {
        if (miniBossPrefab == null)
            return;

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
    }
}