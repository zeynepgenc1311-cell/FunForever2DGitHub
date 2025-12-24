using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    [Header("UI Slot Listesi (sahnedeki slotları ekle)")]
    public List<ItemSlot> uiList = new List<ItemSlot>();

    [Header("Inventory script referansı")]
    public Inventory userInventory;

    private void Awake()
    {
        // Inventory scripti otomatik atama
        if (userInventory == null)
            userInventory = GetComponent<Inventory>();

        if (userInventory == null)
            Debug.LogError("Inventory scripti bulunamadı! UserInventory alanına Inventory scriptini bağla.");
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (userInventory == null)
            return;

        // Her UI slotunu sırayla güncelle
        for (int i = 0; i < uiList.Count; i++)
        {
            ItemSlot uiSlot = uiList[i];
            if (uiSlot == null) continue; // UI slotu boşsa atla

            if (i < userInventory.inventorySlots.Count)
            {
                ItemSlot invSlot = userInventory.inventorySlots[i];
                if (invSlot != null && invSlot.item != null && invSlot.itemCount > 0)
                {
                    // Sprite ve sayıyı güncelle
                    uiSlot.itemImage.sprite = invSlot.item.itemSprite;
                    uiSlot.itemImage.enabled = true;

                    if (invSlot.item.canStackable)
                    {
                        uiSlot.itemCountText.gameObject.SetActive(true);
                        uiSlot.itemCountText.text = invSlot.itemCount.ToString();
                    }
                    else
                        uiSlot.itemCountText.gameObject.SetActive(false);
                }
                else
                {
                    // Slot boşsa UI'yı temizle
                    uiSlot.itemImage.sprite = null;
                    uiSlot.itemImage.enabled = false;
                    uiSlot.itemCountText.gameObject.SetActive(false);
                }
            }
            else
            {
                // inventorySlots listesi uiList'ten kısa ise boş bırak
                uiSlot.itemImage.sprite = null;
                uiSlot.itemImage.enabled = false;
                uiSlot.itemCountText.gameObject.SetActive(false);
            }
        }
    }
}
