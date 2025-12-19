using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipment
}

public enum EquipSlot
{
    Wing,
    Weapon
}

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public ItemType itemType;

    // 🔥 ITEM NEREYE TAKILACAĞINI BİLİYOR
    public EquipSlot equipSlot;

    public GameObject equipPrefab;
}
