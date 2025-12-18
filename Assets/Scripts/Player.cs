using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<ItemSlot> inventorySlots;

    public void AddItem(Item newItem)
    {
        // Boş slot ara
        foreach (ItemSlot slot in inventorySlots)
        {
            if (slot.isEmpty)
            {
                slot.SetItem(newItem);
                Debug.Log(newItem.itemName + " alındı!");
                return;
            }
        }

        Debug.Log("Envanter dolu!");
    }
}

