using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Invenory/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string _itemName;
    [SerializeField] private Sprite _itemImage;
    [SerializeField] private ItemObjectPool _nameItemInObjectPool;

    public string ItemName => _itemName;
    public Sprite ItemImage => _itemImage;
    public ItemObjectPool NameItemInObjectPool => _nameItemInObjectPool;
}
