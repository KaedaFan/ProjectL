using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolForEffect : MonoBehaviour
{
    private static ObjectPoolForEffect _instance;

    public static ObjectPoolForEffect Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Object pool for effect's Instance not found");
            }
            return _instance;
        }
    }

    public List<AbilityType> abilityTypes = new List<AbilityType>();
    private Dictionary<string, Queue<GameObject>> abilityPools;

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
        abilityPools = new Dictionary<string, Queue<GameObject>>();

        foreach (var abilityType in abilityTypes)
        {
            Queue<GameObject> pool = new Queue<GameObject>();

            for (int i = 0; i < abilityType.InitialPoolSize; i++)
            {
                GameObject obj = Instantiate(abilityType.Prefab);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }

            abilityPools.Add(abilityType.Name, pool);
        }
    }

    /// <summary>
    /// Retrieves an effect from the object pool.
    /// </summary>
    /// <param name="abilityName">The name of the object as defined in the pool (via inspector).</param>
    /// <returns>The requested GameObject if available, otherwise null.</returns>
    public GameObject GetEffect(string abilityName)
    {
        if (abilityPools.ContainsKey(abilityName))
        {
            if (abilityPools[abilityName].Count > 0)
            {
                GameObject ability = abilityPools[abilityName].Dequeue();
                ability.SetActive(true);
                return ability;
            }
            else
            {
                Debug.LogWarning($"No available objects in pool '{abilityName}'.");
                return null;
            }
        }
        else
        {
            Debug.LogWarning($"No pool found for effect '{abilityName}'.");
            return null;
        }
    }

    /// <summary>
    /// Returns an effect back to the pool.
    /// </summary>
    /// <param name="abilityName">The name of the effect as defined in the pool (via inspector).</param>
    /// <param name="ability">Reference to the object being returned.</param>
    public void ReturnEffect(string abilityName, GameObject ability)
    {
        if (abilityPools.ContainsKey(abilityName))
        {
            ability.SetActive(false);
            abilityPools[abilityName].Enqueue(ability);
        }
        else
        {
            Debug.LogWarning($"Attempted to return object to non-existent pool '{abilityName}'. Destroying object.");
            Destroy(ability);
        }
    }
}

[System.Serializable]
public class AbilityType
{
    public string Name;
    public GameObject Prefab;
    public int InitialPoolSize;
}
