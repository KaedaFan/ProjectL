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

    public List<EffectType> effectTypes = new List<EffectType>();
    private Dictionary<EffectObjectPool, Queue<GameObject>> _effectPools;

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
        _effectPools = new Dictionary<EffectObjectPool, Queue<GameObject>>();

        foreach (var effectType in effectTypes)
        {
            Queue<GameObject> pool = new Queue<GameObject>();

            for (int i = 0; i < effectType.InitialPoolSize; i++)
            {
                GameObject obj = Instantiate(effectType.Prefab);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }

            _effectPools.Add(effectType.Name, pool);
        }
    }

    /// <summary>
    /// Retrieves an effect from the object pool.
    /// </summary>
    /// <param name="effectName">The name of the object as defined in the pool (via inspector).</param>
    /// <returns>The requested GameObject if available, otherwise null.</returns>
    public GameObject GetEffect(EffectObjectPool effectName)
    {
        if (_effectPools.ContainsKey(effectName))
        {
            if (_effectPools[effectName].Count > 0)
            {
                GameObject effect = _effectPools[effectName].Dequeue();
                effect.SetActive(true);
                return effect;
            }
            else
            {
                Debug.LogWarning($"No available objects in pool '{effectName}'.");
                return null;
            }
        }
        else
        {
            Debug.LogWarning($"No pool found for effect '{effectName}'.");
            return null;
        }
    }

    /// <summary>
    /// Returns an effect back to the pool.
    /// </summary>
    /// <param name="effectName">The name of the effect as defined in the pool (via inspector).</param>
    /// <param name="effect">Reference to the object being returned.</param>
    public void ReturnEffect(EffectObjectPool effectName, GameObject effect)
    {
        if (_effectPools.ContainsKey(effectName))
        {
            effect.SetActive(false);
            _effectPools[effectName].Enqueue(effect);
        }
        else
        {
            Debug.LogWarning($"Attempted to return object to non-existent pool '{effectName}'. Destroying object.");
            Destroy(effect);
        }
    }
}

[System.Serializable]
public class EffectType
{
    public EffectObjectPool Name;
    public GameObject Prefab;
    public int InitialPoolSize;
}
