using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item;
    public Image itemImage;
    public Text countText;
    public Text itemCountText; 

    public bool isEmpty = true;
    private int count = 0;
    public int itemCount
    {
        get { return count; }
    }

    public Button slotButton;

    void Awake()
    {
        ClearSlot();

        if (slotButton == null)
            slotButton = GetComponent<Button>();

        slotButton.onClick.AddListener(OnSlotClicked);
    }

    public void SetItem(Item newItem)
    {
        item = newItem;
        isEmpty = false;
        count = 1;

        itemImage.sprite = item.itemSprite;
        itemImage.enabled = true;

        UpdateCount();
    }

    public void AddItem()
    {
        count++;
        UpdateCount();
    }

    public void RemoveOne()
    {
        count--;
        UpdateCount();

        if (count <= 0)
        {
            ClearSlot();
        }
    }

    void UpdateCount()
    {
        countText.enabled = count > 1;
        countText.text = count.ToString();
    }

    void ClearSlot()
    {
        item = null;
        isEmpty = true;
        count = 0;

        itemImage.enabled = false;
        countText.enabled = false;
    }

    // 🔥 Buraya ekledik: slot tıklandığında equip
    void OnSlotClicked()
    {
        if (item == null) return;

        switch (item.itemType)
        {
            case ItemType.FishingRod:
                PlayerToolController.Instance.EquipFishingRod();
                break;

            case ItemType.Equipment:
                // Diğer ekipmanlar için placeholder
                // PlayerToolController.Instance.EquipEquipment(item);
                break;
        }
    }
}
