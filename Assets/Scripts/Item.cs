using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipment
}

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public ItemType itemType;

    // Equipment için
    public GameObject equipPrefab;
}
