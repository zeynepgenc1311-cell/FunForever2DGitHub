using System.Collections;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public static FishingManager Instance;

    [Header("Mini Game UI Paneli")]
    public GameObject miniGameUI;

    [Header("Balık Listesi")]
    public Item[] fishItems;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // ========================================
    // Bu fonksiyon butondan veya InventoryUIController'dan çağrılır
    // ========================================
    public void CastRod()
    {
        // sadece olta varsa
        if (!PlayerToolController.Instance.HasFishingRod())
            return;

        // mouse pozisyonuna raycast
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (!hit.collider) return;

        if (hit.collider.CompareTag("Water"))
        {
            StartCoroutine(FishingRoutine());
        }
        else
        {
            Debug.Log("Olta yere atıldı, mini game başlamadı");
        }
    }

    // ========================================
    // Mini game mantığı
    // ========================================
    IEnumerator FishingRoutine()
    {
        // balık bekleme süresi (random)
        yield return new WaitForSeconds(Random.Range(2f, 4f));

        if (miniGameUI != null)
            miniGameUI.SetActive(true);

        MiniGameController.Instance.StartMiniGame();

        // mini game bitene kadar bekle
        yield return new WaitUntil(() => MiniGameController.Instance.finished);

        if (miniGameUI != null)
            miniGameUI.SetActive(false);

        // başarılıysa rastgele balık ekle
        if (MiniGameController.Instance.success)
        {
            Item fish = fishItems[Random.Range(0, fishItems.Length)];
            Inventory.Instance.AddItem(fish, 1);
            Debug.Log("Balık yakalandı: " + fish.name);
        }
        else
        {
            Debug.Log("Balık kaçtı");
        }
    }
}
