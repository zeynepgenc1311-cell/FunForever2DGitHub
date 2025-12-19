using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    public Text coinText;
    public Text gemText;
    public Text rainbowCoinText;

    void Update()
    {
        coinText.text = CurrencyManager.Instance.coin.ToString();
        gemText.text = CurrencyManager.Instance.gem.ToString();
        rainbowCoinText.text = CurrencyManager.Instance.rainbowCoin.ToString();
    }
}
