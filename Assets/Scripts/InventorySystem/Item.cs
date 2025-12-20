using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipment
}

public enum EquipSlot
{
    Wing,
    Weapon,
    Shield,
    Helmet,
    Armor
}

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public ItemType itemType;
    public int itemPrice = 1;
    public bool canStackable;
    public CurrencyType costType;

    // 🔥 ITEM NEREYE TAKILACAĞINI BİLİYOR
    public EquipSlot equipSlot;

    public GameObject equipPrefab;
}
