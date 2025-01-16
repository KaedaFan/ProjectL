using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolForEnemy : MonoBehaviour
{
    private static ObjectPoolForEnemy _instance;

    public static ObjectPoolForEnemy Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Object pool for enemy ' s Instance not found");
            }
            return _instance;
        }
    }

    [SerializeField] private List<EnemyType> _enemyTypes;
    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        InitializePools();
    }

    private void InitializePools()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var enemyType in _enemyTypes)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < enemyType.PoolSize; i++)
            {  
                GameObject obj = Instantiate(enemyType.Prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(enemyType.Name, objectPool);
        }
    }

    /// <summary>
    /// Retrieves an enemy object from the pool.
    /// </summary>
    /// <param name="enemyName">The name of the enemy type as defined in the pool.</param>
    /// <param name="position">The position where the enemy should be spawned.</param>
    /// <param name="rotation">The rotation of the enemy when spawned.</param>
    /// <returns>The requested GameObject if available, otherwise null.</returns>
    public GameObject GetEnemy(string enemyName, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(enemyName))
        {
            Debug.LogWarning("This enemy is not in the pool");
            return null;
        }

        if (_poolDictionary[enemyName].Count == 0)
        {
            Debug.LogWarning("No objects available in the pool for the name: " + enemyName);
            return null;
        }

        GameObject objectToSpawn = _poolDictionary[enemyName].Dequeue();


        if (objectToSpawn == null)
        {
            Debug.LogWarning("An object from the pool was destroyed. Creating a new one.");

            var enemyType = _enemyTypes.Find(e => e.Name == enemyName);
            if (enemyType != null)
            {
                objectToSpawn = Instantiate(enemyType.Prefab);
            }
            else
            {
                Debug.LogError("Could not find EnemyType for name: " + enemyName);
                return null;
            }
        }
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        return objectToSpawn;
    }

    /// <summary>
    /// Returns an enemy object back to the pool.
    /// </summary>
    /// <param name="enemyName">The name of the enemy type as defined in the pool.</param>
    /// <param name="enemy">The GameObject to return to the pool.</param>
    public void ReturnEnemy(string enemyName, GameObject enemy)
    {
        if (enemy == null)
        {
            Debug.LogWarning("Attempt to return null object to pool");
            return;
        }

        if (_poolDictionary.ContainsKey(enemyName))
        {
            if (enemy != null)
            {
                enemy.SetActive(false);
                _poolDictionary[enemyName].Enqueue(enemy);
                Debug.Log("Object has been returned to the pool");
            }
            else
            {
                Debug.LogWarning("Attempt to return a destroyed object to the pool");
            }
        }
        else
        {
            Debug.LogWarning("Returned object was not found in the pool");
            Destroy(enemy);
        }
    }



    [System.Serializable]
    public class EnemyType
    {
        public string Name;
        public GameObject Prefab;
        public int PoolSize;
    }
}
