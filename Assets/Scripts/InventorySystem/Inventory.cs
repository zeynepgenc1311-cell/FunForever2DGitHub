using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [Header("Inventory Slots (UI’den sahnede ekle)")]
    public List<ItemSlot> inventorySlots = new List<ItemSlot>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Item ekleme
    public bool AddItem(Item item, int amount = 1)
    {
        if (inventorySlots.Count == 0) 
        {
            Debug.LogError("Inventory slot listesi boş! UI listesine slotları ekle.");
            return false;
        }

        // Stacklenebilir item kontrolü
        foreach (ItemSlot slot in inventorySlots)
        {
            if (slot.item == item && item.canStackable)
            {
                for (int i = 0; i < amount; i++)
                    slot.AddItem();

                return true;
            }
        }

        // Boş slot bul
        foreach (ItemSlot slot in inventorySlots)
        {
            if (slot.item == null)
            {
                slot.SetItem(item);
                return true;
            }
        }

        Debug.Log("Envanter dolu!");
        return false;
    }

    // Item silme
    public bool RemoveItem(Item item, int amount)
    {
        foreach (ItemSlot slot in inventorySlots)
        {
            if (slot.item == item)
            {
                for (int i = 0; i < amount; i++)
                    slot.RemoveOne();
                return true;
            }
        }
        return false;
    }
}
