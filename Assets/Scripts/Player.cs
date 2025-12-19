using UnityEngine;

public class Player : MonoBehaviour
{
    public ItemSlot[] slots;

    [Header("Equip Points")]
    public Transform wingPoint;
    public Transform handPoint;

    private GameObject equippedWing;
    private GameObject equippedSword;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            UseSlot(0); // Kanat

        if (Input.GetKeyDown(KeyCode.Alpha2))
            UseSlot(1); // Kılıç
    }

    // ================= INVENTORY =================

    public void AddItem(Item newItem)
    {
        foreach (ItemSlot slot in slots)
        {
            if (!slot.isEmpty && slot.item == newItem)
            {
                slot.AddItem();
                return;
            }
        }

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

        // 🔥 SLOT 0 = KANAT
        if (index == 0)
        {
            ToggleEquip(ref equippedWing, wingPoint, item.equipPrefab);
        }
        // 🔥 SLOT 1 = KILIÇ
        else if (index == 1)
        {
            ToggleEquip(ref equippedSword, handPoint, item.equipPrefab);
        }
    }

    // ================= EQUIP CORE =================

    void ToggleEquip(ref GameObject equippedObj, Transform point, GameObject prefab)
    {
        if (point == null || prefab == null)
        {
            Debug.LogError("Equip point veya prefab eksik!");
            return;
        }

        if (equippedObj != null)
        {
            Destroy(equippedObj);
            equippedObj = null;
        }
        else
        {
            equippedObj = Instantiate(prefab, point);
            equippedObj.transform.localPosition = Vector3.zero;
            equippedObj.transform.localRotation = Quaternion.identity;
            equippedObj.transform.localScale = Vector3.one;
        }
    }
}
