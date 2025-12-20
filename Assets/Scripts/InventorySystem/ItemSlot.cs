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


    void Awake()
    {
        ClearSlot();
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
}
