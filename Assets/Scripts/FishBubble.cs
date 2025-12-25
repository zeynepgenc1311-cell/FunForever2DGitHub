using UnityEngine;
using UnityEngine.UI;

public class FishBubble : MonoBehaviour
{
    [Header("UI Components")]
    public Image iconImage; // balık resmi
    public Text nameText;   // balık ismi + yakaladın

    // Balığı ayarlamak için çağır
    public void Setup(Item fish)
    {
        if (iconImage != null)
            iconImage.sprite = fish.itemSprite;

        if (nameText != null)
            nameText.text = fish.itemName + " yakaladın!";
    }
}
