using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private Transform _parentForSlots;
    [SerializeField] private GameObject _slotPrefab;

    private Inventory _inventory;

    private void Start()
    {
        _inventory = Inventory.Instance;
        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (Transform slot in _parentForSlots)
        {
            Destroy(slot.gameObject);
        }

        foreach (var inventoryItem in _inventory.items)
        {
            var slot = Instantiate(_slotPrefab, _parentForSlots);
            var image = slot.transform.Find("Image").GetComponent<Image>();
            var countText = slot.transform.Find("ItemCount").GetComponent<TMP_Text>();

            image.sprite = inventoryItem.Item.ItemImage;
            countText.text = inventoryItem.Count > 1 ? inventoryItem.Count.ToString() : " ";
        }
    }

    public void OnDeleteButtonPressed(Item item)
    {
        _inventory.RemoveItem(item);
        UpdateUI();
    }

}
