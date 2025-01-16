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

    private InventoryUI _inventoryUI;
    public List<InventoryItem> items = new List<InventoryItem>();

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
        _inventoryUI = GetComponent<InventoryUI>();
        
    }

    private void Start()
    {
        LoadFromDatabase();
    }


    /// <summary>
    /// Adding an item to inventory
    /// </summary>
    /// <param name="item">Reference to SO object item</param</param>
    /// <param name="count">Count of items in inventory</param>
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

        SaveToDatabase(); 
        _inventoryUI.UpdateUI();
    }

    /// <summary>
    /// Removing an item from inventory
    /// </summary>
    /// <param name="item">Reference to SO object item</param>
    public void RemoveItem(Item item)
    {
        var existingItem = items.Find(i => i.Item == item);
        if (existingItem != null)
        {
            items.Remove(existingItem);
            SaveToDatabase(); 
            _inventoryUI.UpdateUI();
        }
    }

    public void SaveToDatabase()
    {
        var realm = DatabaseManager.Instance.GetRealmInstance();

        realm.Write(() =>
        {
            foreach (var inventoryItem in items)
            {
                var dbItem = realm.Find<InventoryItemModel>(inventoryItem.Item.Id);
                if (dbItem != null)
                {
                    dbItem.Count = inventoryItem.Count;
                }
                else
                {
                    realm.Add(new InventoryItemModel
                    {
                        ItemId = inventoryItem.Item.Id,
                        Count = inventoryItem.Count
                    });
                }
            }
        });
    }

    public void LoadFromDatabase()
    {
        var realm = DatabaseManager.Instance.GetRealmInstance();
        items.Clear();

        foreach (var dbItem in realm.All<InventoryItemModel>())
        {
            var itemSO = Resources.Load<Item>($"Items/{dbItem.ItemId}");
            if (itemSO != null)
            {
                items.Add(new InventoryItem { Item = itemSO, Count = dbItem.Count });
            }
            else
            {
                Debug.LogWarning($"Item with ID {dbItem.ItemId} not found in Resources.");
            }
        }

        _inventoryUI.UpdateUI();
    }

}

[System.Serializable]
public class InventoryItem
{
    public Item Item;
    public int Count;
}