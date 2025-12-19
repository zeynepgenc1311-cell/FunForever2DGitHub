using UnityEngine;

public class Player : MonoBehaviour
{
    public ItemSlot[] slots;

    public Transform wingPoint;
    private GameObject equippedWing;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseSlot(0);
        }
    }

    public void AddItem(Item newItem)
    {
        // Aynı item varsa stackle
        foreach (ItemSlot slot in slots)
        {
            if (!slot.isEmpty && slot.item == newItem)
            {
                slot.AddItem();
                return;
            }
        }

        // Boş slot bul
        foreach (ItemSlot slot in slots)
        {
            if (slot.isEmpty)
            {
                slot.SetItem(newItem);
                return;
            }
        }

        Debug.Log("Envanter dolu 😅");
    }

    void UseSlot(int index)
    {
        if (index >= slots.Length) return;

        ItemSlot slot = slots[index];
        if (slot.isEmpty) return;

        Item item = slot.item;

        if (item.itemType == ItemType.Equipment)
        {
            ToggleEquip(item);
        }
    }

    void ToggleEquip(Item item)
    {
        if (equippedWing != null)
        {
            Destroy(equippedWing);
            equippedWing = null;
            Debug.Log("Kanat çıkarıldı 🪽");
        }
        else
        {
            equippedWing = Instantiate(item.equipPrefab, wingPoint);
            Debug.Log("Kanat takıldı 😎");
        }
    }
}
