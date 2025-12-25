using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum FishingState
{
    Idle,
    WaitingFish,
    CanReel,
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

    [Header("Olta Objeleri")]
    public GameObject fishingRodInHand;
    public GameObject fishingRodOnBack;

    [Header("Kafada Gösterilecek Objeler (Sahnedeki)")]
    public GameObject fishVisual;       // HeadPoint altına child, başta inactive
    public GameObject fishBubble;       // fishVisual altına child panel, başta inactive

    public Player player;

    public FishingState state = FishingState.Idle;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Başta görünmesinler
        if (fishVisual != null) fishVisual.SetActive(false);
        if (fishBubble != null) fishBubble.SetActive(false);
    }

    public void OnFishingButton()
    {
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

    IEnumerator WaitForFish()
    {
        state = FishingState.WaitingFish;
        yield return new WaitForSeconds(Random.Range(15f, 20f));

        state = FishingState.CanReel;
        questionMark.SetActive(true);

        yield return new WaitForSeconds(2f);

        if (state == FishingState.CanReel)
        {
            Debug.Log("⏰ Geç kaldın balık kaçtı");
            ResetFishing();
        }
    }

    void StartMiniGame()
    {
        state = FishingState.MiniGame;
        questionMark.SetActive(false);

        // Olta elimizden kalkıyor
        fishingRodInHand.SetActive(false);

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

            // FishVisual ve Bubble aktif et
            if (fishVisual != null)
            {
                fishVisual.SetActive(true);
                FishVisual visualScript = fishVisual.GetComponent<FishVisual>();
                if (visualScript != null)
                    visualScript.Setup(fish); // sprite değişecek
            }

            if (fishBubble != null)
            {
                fishBubble.SetActive(true);
                FishBubble bubbleScript = fishBubble.GetComponent<FishBubble>();
                if (bubbleScript != null)
                    bubbleScript.Setup(fish); // text ve icon değişecek
            }
        }
        else
        {
            Debug.Log("💔 Balık kaçtı");
        }

        ResetFishing();
    }

    // Ekrana tıklayınca olta geri gelir, kafadaki balık ve baloncuk kaybolur
    public void OnScreenTap()
    {
        if (fishVisual != null)
        {
            fishVisual.SetActive(false);
        }

        if (fishBubble != null)
        {
            fishBubble.SetActive(false);
        }

        if (fishingRodInHand != null)
        {
            fishingRodInHand.SetActive(true);
        }
    }

    void ResetFishing()
    {
        state = FishingState.Idle;
        questionMark.SetActive(false);

        if (fishingRodInHand != null)
            fishingRodInHand.SetActive(true);
    }
}
