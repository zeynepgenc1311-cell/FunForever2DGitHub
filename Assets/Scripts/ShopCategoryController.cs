using UnityEngine;

public class ShopCategoryController : MonoBehaviour
{
    public GameObject clothesPanel;
    public GameObject hatsPanel;
    public GameObject shoesPanel;
    public GameObject handPanel;
    public GameObject wingsPanel;

    void Start()
    {
        ShowClothes();
    }

    void HideAll()
    {
        clothesPanel.SetActive(false);
        hatsPanel.SetActive(false);
        shoesPanel.SetActive(false);
        handPanel.SetActive(false);
        wingsPanel.SetActive(false);
    }

    public void ShowClothes()
    {
        HideAll();
        clothesPanel.SetActive(true);
    }

    public void ShowHats()
    {
        HideAll();
        hatsPanel.SetActive(true);
    }

    public void ShowShoes()
    {
        HideAll();
        shoesPanel.SetActive(true);
    }

    public void ShowHand()
    {
        HideAll();
        handPanel.SetActive(true);
    }

    public void ShowWings()
    {
        HideAll();
        wingsPanel.SetActive(true);
    }
}
