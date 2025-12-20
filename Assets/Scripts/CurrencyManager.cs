using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    [Header("Para Miktarları")]
    public int coins;
    public int gems;
    public int rainbowCoins;

    [Header("UI")]
    public Text coinText;
    public Text gemText;
    public Text rainbowCoinText;

    [Header("Debug")]
    public bool resetOnStart = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (resetOnStart)
        {
            PlayerPrefs.DeleteKey("coins");
            PlayerPrefs.DeleteKey("gems");
            PlayerPrefs.DeleteKey("rainbowCoins");
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs sıfırlandı");
        }

        if (!PlayerPrefs.HasKey("coins"))
            PlayerPrefs.SetInt("coins", coins);

        if (!PlayerPrefs.HasKey("gems"))
            PlayerPrefs.SetInt("gems", gems);

        if (!PlayerPrefs.HasKey("rainbowCoins"))
            PlayerPrefs.SetInt("rainbowCoins", rainbowCoins);

        coins = PlayerPrefs.GetInt("coins");
        gems = PlayerPrefs.GetInt("gems");
        rainbowCoins = PlayerPrefs.GetInt("rainbowCoins");
    }

    void Start()
    {
        UpdateUI();
    }

    // -------- ADD --------

    public void AddCoins(int amount)
    {
        coins += amount;
        PlayerPrefs.SetInt("coins", coins);
        UpdateUI();
    }

    public void AddGems(int amount)
    {
        gems += amount;
        PlayerPrefs.SetInt("gems", gems);
        UpdateUI();
    }

    public void AddRainbowCoins(int amount)
    {
        rainbowCoins += amount;
        PlayerPrefs.SetInt("rainbowCoins", rainbowCoins);
        UpdateUI();
    }

    // -------- SPEND --------

    public bool SpendCoins(int amount)
    {
        if (coins < amount) return false;

        coins -= amount;
        PlayerPrefs.SetInt("coins", coins);
        UpdateUI();
        return true;
    }

    public bool SpendGems(int amount)
    {
        if (gems < amount) return false;

        gems -= amount;
        PlayerPrefs.SetInt("gems", gems);
        UpdateUI();
        return true;
    }

    public bool SpendRainbowCoins(int amount)
    {
        if (rainbowCoins < amount) return false;

        rainbowCoins -= amount;
        PlayerPrefs.SetInt("rainbowCoins", rainbowCoins);
        UpdateUI();
        return true;
    }

    // -------- UI --------

    public void UpdateUI()
    {
        if (coinText != null)
            coinText.text = coins.ToString();

        if (gemText != null)
            gemText.text = gems.ToString();

        if (rainbowCoinText != null)
            rainbowCoinText.text = rainbowCoins.ToString();
    }
}
