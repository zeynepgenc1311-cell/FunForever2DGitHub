using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject sellContent;
    [SerializeField] private List<Item> sellItems = new();
    [SerializeField] private ShopSlot ShopSlotPrefab;
    


    private void Start()
    {
        SellFillSlots(); 
    }

    public ShopState currentState = ShopState.Sell;

    public void MarketRequest(Item item, int amount)
    {
        switch (currentState)
        {
            case ShopState.Sell:
            bool condition = Inventory.Instance.RemoveItem(item, amount);
            if (condition)
            {
                CurrencyManager.Instance.AddCoins(item.itemPrice);
            }
            else Debug.Log("item sende yok satılmadı");
            break;
        }
    }


    public void SellFillSlots()
    {
        foreach (Item item in sellItems)
        {
            ShopSlot slot = Instantiate(ShopSlotPrefab, sellContent.transform.position, Quaternion.identity);
            slot.transform.SetParent(sellContent.transform);
            slot.Initialize(item);
        }
    }
    
    public static ShopManager Instance;

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
    Sell
}