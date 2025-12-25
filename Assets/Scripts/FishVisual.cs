using UnityEngine;
using UnityEngine.UI;

public class FishVisual : MonoBehaviour
{
    public Image fishImage; // UI Image

    public void Setup(Item fish)
    {
        if (fishImage != null && fish.itemSprite != null)
        {
            fishImage.sprite = fish.itemSprite;
            fishImage.color = Color.white; // Alfa 0’dıysa görünür yap
        }
    }
}
