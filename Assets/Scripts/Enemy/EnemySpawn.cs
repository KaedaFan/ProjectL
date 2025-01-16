using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Vector2 _spawnAreaMin; 
    [SerializeField] private Vector2 _spawnAreaMax;
    [SerializeField] private int _countEnemy = 3;
    private void Start()
    {
        SpawnRandomEnemies(_countEnemy);
    }

    public void SpawnRandomEnemies(int count)
    {
        if (ObjectPoolForEnemy.Instance == null)
        {
            Debug.LogError("ObjectPoolForEnemy instance is not initialized!");
            return;
        }

        List<ObjectPoolForEnemy.EnemyType> enemyTypes = ObjectPoolForEnemy.Instance.GetEnemyTypes();

        if (enemyTypes == null || enemyTypes.Count == 0)
        {
            Debug.LogWarning("No enemy types available in the pool!");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            ObjectPoolForEnemy.EnemyType randomEnemyType = enemyTypes[Random.Range(0, enemyTypes.Count)];

            Vector2 randomPosition = new Vector2(
                Random.Range(_spawnAreaMin.x, _spawnAreaMax.x),
                Random.Range(_spawnAreaMin.y, _spawnAreaMax.y)
            );

            GameObject spawnedEnemy = ObjectPoolForEnemy.Instance.GetEnemy(randomEnemyType.Name, randomPosition, Quaternion.identity);

            if (spawnedEnemy != null)
            {
                Debug.Log($"Spawned enemy: {randomEnemyType.Name} at position {randomPosition}");
            }
        }
    }
}
