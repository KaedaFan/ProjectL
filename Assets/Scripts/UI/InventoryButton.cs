using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel;

    private void Start()
    {
        _inventoryPanel.SetActive(false);
    }

    public void OpenCloseInventory()
    {
        if (_inventoryPanel.activeSelf) CloseInventory();
        else OpenInventory();
    }

    private void OpenInventory()
    {
        _inventoryPanel.SetActive(true);
    }

    private void CloseInventory()
    {
        _inventoryPanel.SetActive(false);
    }
}
