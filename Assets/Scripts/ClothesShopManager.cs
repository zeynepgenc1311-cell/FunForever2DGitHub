using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClothesShopManager : MonoBehaviour
{
    [SerializeField] private GameObject buyContent;
    [SerializeField] private List<Item> buyItems = new();
    [SerializeField] private ShopSlot ShopSlotPrefab;
    


    private void Start()
    {
        BuyFillSlots();
    }

    public ShopState currentState = ShopState.Buy;

    public void MarketRequest(Item item, int amount)
    {
        switch (currentState)
        {
            case ShopState.Buy:
            bool enoughMoney = CurrencyManager.Instance.SpendCoins(item.itemPrice);
            if (enoughMoney)
            {
                bool success = Inventory.Instance.AddItem(item, amount);
                    if (!success)
                        Debug.Log("Envanter dolu.");
            }
            else Debug.Log("paran yetmiyo");
            break;
        }
    }


    public void BuyFillSlots()
    {
        foreach (Item item in buyItems)
        {
            ShopSlot slot = Instantiate(ShopSlotPrefab, buyContent.transform.position, Quaternion.identity);
            slot.transform.SetParent(buyContent.transform);
            slot.Initialize(item);
        }
    }
    
    public static ClothesShopManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}

[System.Serializable]
public enum ShopState
{
    Buy
}