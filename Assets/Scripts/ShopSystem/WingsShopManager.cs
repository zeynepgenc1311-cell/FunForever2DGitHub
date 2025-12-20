using System.Collections.Generic;
using UnityEngine;

public class WingsShopManager : MonoBehaviour
{
    public static WingsShopManager Instance;

    [SerializeField] private Transform buyContent;
    [SerializeField] private List<Item> buyItems = new();
    [SerializeField] private WingsShopSlot wingsShopSlotPrefab;

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
            WingsShopSlot slot = Instantiate(wingsShopSlotPrefab, buyContent);
            slot.Initialize(item);
        }
    }
}
