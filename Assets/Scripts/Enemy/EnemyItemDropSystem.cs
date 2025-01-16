using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemDropSystem : MonoBehaviour
{
    private static EnemyItemDropSystem _instance;

    public static EnemyItemDropSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Enemy item drop system ' s Instance not found");
            }
            return _instance;
        }
    }

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
    }

    public void ItemDropFromEnemy(ItemObjectPool itemNameInObjectPool, Vector2 position)
    {
        GameObject newItem = ObjectPoolForItemDrop.Instance.GetItemDrop(itemNameInObjectPool, position);
    }
}
