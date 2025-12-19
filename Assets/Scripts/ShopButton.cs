using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public GameObject shopPanel;

    public void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }
}
