using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    
    [SerializeField] private Item _itemType;
    [SerializeField] private int _countItemInDrop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_itemType == null)
        {
            Debug.LogError($"Item {gameObject.name} don't have Item type");
            return;
        }

        if (collision.CompareTag("Player"))
        {
            ObjectPoolForItemDrop.Instance.ReturnItemDrop(_itemType.NameItemInObjectPool, gameObject);
            Inventory.Instance.AddItem(_itemType, _countItemInDrop);
        }
    }
}
