using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory _instance;

    public static Inventory Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Inventory Instance not found");
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

    public List<InventoryItem> items = new List<InventoryItem>();

    /// <summary>
    /// Добавление предмета в инвентарь
    /// </summary>
    /// <param name="item">Ссылка на SO объект предмета</param>
    /// <param name="count">Количество предмета в инвентаре</param>
    public void AddItem(Item item, int count = 1)
    {
        var existingItem = items.Find(i => i.Item == item);
        if (existingItem != null)
        {
            existingItem.Count += count;
        }
        else
        {
            items.Add(new InventoryItem { Item = item, Count = count });
        }
    }

    /// <summary>
    /// Удаление предмета из инвентаря
    /// </summary>
    /// <param name="item">Ссылка на SO объект предмета</param>
    public void RemoveItem(Item item)
    {
        var existingItem = items.Find(i => i.Item == item);
        if (existingItem != null)
        {
            items.Remove(existingItem);
        }
    }
}

[System.Serializable]
public class InventoryItem
{
    public Item Item;
    public int Count;
}
