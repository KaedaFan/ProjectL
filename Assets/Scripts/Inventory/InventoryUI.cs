using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private Transform _parentForSlots;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _deleteButton; 

    private Inventory _inventory;
    private Item _selectedItem; 

    private void Start()
    {
        _inventory = Inventory.Instance;
        UpdateUI();
        _deleteButton.SetActive(false);
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

            var button = slot.GetComponent<Button>();
            button.onClick.AddListener(() => OnSlotClicked(inventoryItem.Item));
        }
    }

    private void OnSlotClicked(Item item)
    {
        _selectedItem = item;
        _deleteButton.SetActive(true); 
    }

    public void OnDeleteButtonPressed()
    {
        if (_selectedItem != null)
        {
            _inventory.RemoveItem(_selectedItem);
            UpdateUI(); 
            _deleteButton.SetActive(false); 
            _selectedItem = null; 
        }
    }
}