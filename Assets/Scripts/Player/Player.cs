using UnityEngine;

public class Player : MonoBehaviour
{
    public ItemSlot[] slots;

    [Header("Equip Points")]
    public Transform wingPoint;
    public Transform handPoint;
    public Transform shieldPoint;
    public Transform helmetPoint;
    public Transform armorPoint;

    private GameObject equippedWing;
    private GameObject equippedWeapon;
    private GameObject equippedShield;
    private GameObject equippedHelmet;
    private GameObject equippedArmor;
    private Item equippedWeaponItem;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) UseSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) UseSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) UseSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) UseSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) UseSlot(4);
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
    

    // ================= HOTBAR =================

    void UseSlot(int index)
    {
        if (index < 0 || index >= slots.Length) return;

        ItemSlot slot = slots[index];
        if (slot.isEmpty || slot.item == null) return;

        Item item = slot.item;
        if (item.itemType != ItemType.Equipment) return;

        switch (item.equipSlot)
        {
            case EquipSlot.Wing:
                ToggleEquip(ref equippedWing, wingPoint, item.equipPrefab);
                break;

            case EquipSlot.Weapon:
                ToggleEquip(ref equippedWeapon, handPoint, item.equipPrefab);
                if (equippedWeapon != null)
                equippedWeaponItem = item;
                else
                equippedWeaponItem = null;
                break;

            case EquipSlot.Shield:
                ToggleEquip(ref equippedShield, shieldPoint, item.equipPrefab);
                break;

            case EquipSlot.Helmet:
                ToggleEquip(ref equippedHelmet, helmetPoint, item.equipPrefab);
                break;

            case EquipSlot.Armor:
                ToggleEquip(ref equippedArmor, armorPoint, item.equipPrefab);
                break;
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

    public bool HasFishingRodEquipped()
{
    return equippedWeapon != null;
}

public bool HasFishingRod()
{
    Debug.Log("HasFishingRod çağrıldı | equippedWeaponItem: " + equippedWeaponItem);
    return equippedWeaponItem != null && equippedWeaponItem.isFishingRod;
}


}
