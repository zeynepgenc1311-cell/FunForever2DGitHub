using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public PlayerInventoryData playerInventory;
    private InventoryUIController inventoryUI;

    public static Inventory Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        inventoryUI = GetComponent<InventoryUIController>();
        inventoryUI.UpdateUI();
    }

    // ITEM EKLEME
    public bool AddItem(Item item, int amount = 1)
    {
        // Önce stacklenebilir ve aynı item varsa ekle
        for (int i = 0; i < playerInventory.inventorySlots.Count; i++)
        {
            ItemSlot slot = playerInventory.inventorySlots[i];

            if (slot.item == item && item.canStackable)
            {
                for (int j = 0; j < amount; j++)
                {
                    slot.AddItem();
                }

                inventoryUI.UpdateUI();
                return true;
            }
        }

        // Boş slot bul
        for (int i = 0; i < playerInventory.inventorySlots.Count; i++)
        {
            ItemSlot slot = playerInventory.inventorySlots[i];

            if (slot.item == null)
            {
                slot.SetItem(item);
                inventoryUI.UpdateUI();
                return true;
            }
        }

        Debug.Log("ENVANTER DOLU!");
        return false;
    }

    // ITEM SİLME
    public bool RemoveItem(Item item, int amount)
    {
        for (int i = 0; i < playerInventory.inventorySlots.Count; i++)
        {
            ItemSlot slot = playerInventory.inventorySlots[i];

            if (slot.item == item)
            {
                for (int j = 0; j < amount; j++)
                {
                    slot.RemoveOne();
                }

                inventoryUI.UpdateUI();
                return true;
            }
        }

        return false;
    }

    // WORLD'DEN ITEM ALMA
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Item worldItem = other.GetComponent<Item>();
            if (worldItem == null) return;

            if (AddItem(worldItem))
            {
                Destroy(other.gameObject);
                inventoryUI.UpdateUI();
            }
        }
    }
}
