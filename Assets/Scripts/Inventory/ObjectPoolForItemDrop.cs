using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolForItemDrop : MonoBehaviour
{
    private static ObjectPoolForItemDrop _instance;

    public static ObjectPoolForItemDrop Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Object pool for item drop ' s Instance not found");
            }
            return _instance;
        }
    }

    [SerializeField] private List<ItemDropType> _itemDropsType;
    private Dictionary<ItemObjectPool, Queue<GameObject>> _poolDictionary;

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
        _poolDictionary = new Dictionary<ItemObjectPool, Queue<GameObject>>();

        foreach (var itemType in _itemDropsType)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < itemType.PoolSize; i++)
            {
                GameObject obj = Instantiate(itemType.Prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(itemType.Name, objectPool);
        }
    }

    /// <summary>
    /// Retrieves an item drop object from the pool.
    /// </summary>
    /// <param name="itemName">The name of the item drop type as defined in the pool.</param>
    /// <param name="position">The position where the item drop should be spawned.</param>
    /// <param name="rotation">The rotation of the item drop when spawned.</param>
    /// <returns>The requested GameObject if available, otherwise null.</returns>
    public GameObject GetItemDrop(ItemObjectPool itemName, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(itemName))
        {
            Debug.LogWarning("This item drop is not in the pool");
            return null;
        }

        if (_poolDictionary[itemName].Count == 0)
        {
            Debug.LogWarning("No objects available in the pool for the name: " + itemName);
            return null;
        }

        GameObject objectToSpawn = _poolDictionary[itemName].Dequeue();


        if (objectToSpawn == null)
        {
            Debug.LogWarning("An object from the pool was destroyed. Creating a new one.");

            var itemDropType = _itemDropsType.Find(e => e.Name == itemName);
            if (itemDropType != null)
            {
                objectToSpawn = Instantiate(itemDropType.Prefab);
            }
            else
            {
                Debug.LogError("Could not find ItemDropType for name: " + itemName);
                return null;
            }
        }
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        return objectToSpawn;
    }

    /// <summary>
    /// Returns an item drop object back to the pool.
    /// </summary>
    /// <param name="itemDropName">The name of the item drop type as defined in the pool.</param>
    /// <param name="itemDrop">The GameObject to return to the pool.</param>
    public void ReturnItemDrop(ItemObjectPool itemDropName, GameObject itemDrop)
    {
        if (itemDrop == null)
        {
            Debug.LogWarning("Attempt to return null object to pool");
            return;
        }

        if (_poolDictionary.ContainsKey(itemDropName))
        {
            if (itemDrop != null)
            {
                itemDrop.SetActive(false);
                _poolDictionary[itemDropName].Enqueue(itemDrop);
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
            Destroy(itemDrop);
        }
    }
}

[System.Serializable]
public class ItemDropType
{
    public ItemObjectPool Name;
    public GameObject Prefab;
    public int PoolSize;
}