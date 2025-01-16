using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string _id; 
    [SerializeField] private string _itemName; 
    [SerializeField] private Sprite _itemImage; 
    [SerializeField] private string _description; 
    [SerializeField] private ItemObjectPool _nameItemInObjectPool;

    public string Id => _id;
    public string ItemName => _itemName;
    public Sprite ItemImage => _itemImage;
    public string Description => _description;
    public ItemObjectPool NameItemInObjectPool => _nameItemInObjectPool;
}
