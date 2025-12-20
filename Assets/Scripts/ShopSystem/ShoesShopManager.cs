using System.Collections.Generic;
using UnityEngine;

public class ShoesShopManager : MonoBehaviour
{
    public static ShoesShopManager Instance;

    [SerializeField] private Transform buyContent;
    [SerializeField] private List<Item> buyItems = new();
    [SerializeField] private ShoesShopSlot shoesShopSlotPrefab;

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
            ShoesShopSlot slot = Instantiate(shoesShopSlotPrefab, buyContent);
            slot.Initialize(item);
        }
    }
}
