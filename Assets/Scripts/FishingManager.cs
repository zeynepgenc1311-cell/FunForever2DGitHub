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

    [Header("Baloncuk Prefab")]
    public GameObject fishBubblePrefab;

    [Header("UI")]
    public GameObject questionMark;

    [Header("Olta Objeleri")]
    public GameObject fishingRodInHand;
    public GameObject fishingRodOnBack;

    public Player player;

    public FishingState state = FishingState.Idle;

    private GameObject currentFishVisual;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
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

            // Balık kafanın üstünde spawn
            currentFishVisual = Instantiate(fish.equipPrefab);
            currentFishVisual.transform.SetParent(player.headPoint);
            currentFishVisual.transform.localPosition = new Vector3(0, 0.5f, 0);
            currentFishVisual.transform.localRotation = Quaternion.identity;
            currentFishVisual.transform.localScale = Vector3.one * 0.5f;

            // Baloncuk spawn
            if (fishBubblePrefab != null)
            {
                GameObject bubble = Instantiate(fishBubblePrefab, currentFishVisual.transform);
                bubble.transform.localPosition = new Vector3(0, 0.5f, 0);
                bubble.transform.localRotation = Quaternion.identity;
                bubble.transform.localScale = Vector3.one;

                FishBubble bubbleScript = bubble.GetComponent<FishBubble>();
                if (bubbleScript != null)
                    bubbleScript.Setup(fish);
            }
        }
        else
        {
            Debug.Log("💔 Balık kaçtı");
        }

        ResetFishing();
    }

    // Ekrana tıklayınca olta geri gelir, balık kafada kalır
    public void OnScreenTap()
    {
        if (currentFishVisual != null)
        {
            fishingRodInHand.SetActive(true);
            Destroy(currentFishVisual, 0.1f);
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
