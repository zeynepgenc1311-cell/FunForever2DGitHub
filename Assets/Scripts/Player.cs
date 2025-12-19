using UnityEngine;

public class Player : MonoBehaviour
{
    public ItemSlot[] slots;

    [Header("Equipment")]
    public Transform wingPoint;
    private GameObject equippedWing;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseSlot(0);
        }
    }

    // ================= INVENTORY =================

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
        if (index < 0 || index >= slots.Length) return;

        ItemSlot slot = slots[index];
        if (slot.isEmpty || slot.item == null) return;

        Item item = slot.item;

        if (item.itemType == ItemType.Equipment)
        {
            ToggleEquip(item);
        }
    }

    // ================= EQUIP =================

    void ToggleEquip(Item item)
    {
        if (wingPoint == null)
        {
            Debug.LogError("WingPoint atanmadı!");
            return;
        }

        if (equippedWing != null)
        {
            Destroy(equippedWing);
            equippedWing = null;
            Debug.Log("Kanat çıkarıldı 🪽");
        }
        else
        {
            if (item.equipPrefab == null)
            {
                Debug.LogError("Equip Prefab boş!");
                return;
            }

            equippedWing = Instantiate(item.equipPrefab, wingPoint);
            equippedWing.transform.localPosition = Vector3.zero;
            equippedWing.transform.localRotation = Quaternion.identity;
            equippedWing.transform.localScale = Vector3.one;

            Debug.Log("Kanat takıldı 😎");
        }
    }
}
