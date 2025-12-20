using System.Collections.Generic;
using UnityEngine;

public class HandShopManager : MonoBehaviour
{
    public static HandShopManager Instance;

    [SerializeField] private Transform buyContent;
    [SerializeField] private List<Item> buyItems = new();
    [SerializeField] private HandShopSlot handShopSlotPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        BuyFillSlots();
    }

    public void MarketRequest(Item item, int amount)
    {
        bool paid = CurrencyManager.Instance.SpendCurrency(
            item.costType,
            item.itemPrice
        );

        if (!paid)
        {
            Debug.Log("Paran yetmiyor");
            return;
        }

        bool added = Inventory.Instance.AddItem(item, amount);
        if (!added)
            Debug.Log("Envanter dolu");
    }

    private void BuyFillSlots()
    {
        foreach (Item item in buyItems)
        {
            HandShopSlot slot = Instantiate(handShopSlotPrefab, buyContent);
            slot.Initialize(item);
        }
    }
}
