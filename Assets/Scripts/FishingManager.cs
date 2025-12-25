using System.Collections;
using UnityEngine;

public enum FishingState
{
    Idle,        // hiçbir şey yok
    WaitingFish, // balık bekleniyor
    CanReel,     // ❓ çıktı
    MiniGame
}

public class FishingManager : MonoBehaviour
{
    public static FishingManager Instance;

    [Header("Mini Game UI")]
    public GameObject miniGameUI;

    [Header("Balık Listesi")]
    public Item[] fishItems;

    [Header("UI")]
    public GameObject questionMark;

    public Player player;

    public FishingState state = FishingState.Idle;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // ===============================
    // BUTON BURAYI ÇAĞIRACAK
    // ===============================
    public void OnFishingButton()
    {
        Debug.Log("🟡 Fishing button basıldı");

    if (player == null)
    {
        Debug.LogError("❌ Player referansı yok");
        return;
    }

    if (!player.HasFishingRod())
    {
        Debug.Log("❌ Olta equip değil");
        return;
    }

        if (!PlayerFishing.Instance.CanFish())
        {
            Debug.Log("Suyun yanında değilsin");
            return;
        }

        if (state == FishingState.Idle)
        {
            Debug.Log("🎣 OLTA ATILDI");
            StartCoroutine(WaitForFish());
        }
        else if (state == FishingState.CanReel)
        {
            StartMiniGame();
        }
    }

    // ===============================
    // BALIK BEKLEME
    // ===============================
    IEnumerator WaitForFish()
    {
        state = FishingState.WaitingFish;

        yield return new WaitForSeconds(Random.Range(15f, 20f));

        state = FishingState.CanReel;
        questionMark.SetActive(true);

        // refleks süresi
        yield return new WaitForSeconds(2f);

        if (state == FishingState.CanReel)
        {
            Debug.Log("⏰ Geç kaldın balık kaçtı");
            ResetFishing();
        }
    }

    // ===============================
    // MINIGAME BAŞLAT
    // ===============================
    void StartMiniGame()
    {
        state = FishingState.MiniGame;
        questionMark.SetActive(false);

        miniGameUI.SetActive(true);
        MiniGameController.Instance.StartMiniGame();

        StartCoroutine(WaitMiniGameEnd());
    }

    IEnumerator WaitMiniGameEnd()
    {
        yield return new WaitUntil(() => MiniGameController.Instance.finished);

        miniGameUI.SetActive(false);

        if (MiniGameController.Instance.success)
        {
            Item fish = fishItems[Random.Range(0, fishItems.Length)];
            Inventory.Instance.AddItem(fish, 1);
            Debug.Log("🐟 Balık yakalandı: " + fish.name);
        }
        else
        {
            Debug.Log("💔 Balık kaçtı");
        }

        ResetFishing();
    }

    void ShowQuestionMark()
{
    if (questionMark != null)
        questionMark.SetActive(true);
}
    void HideQuestionMark()
{
    if (questionMark != null)
        questionMark.SetActive(false);
}


    void ResetFishing()
    {
        state = FishingState.Idle;
        questionMark.SetActive(false);
    }
}
