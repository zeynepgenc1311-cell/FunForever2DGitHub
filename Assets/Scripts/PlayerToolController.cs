using UnityEngine;

public enum ToolType
{
    None,
    FishingRod
}

public class PlayerToolController : MonoBehaviour
{
    public static PlayerToolController Instance;

    public ToolType currentTool = ToolType.None;

    [Header("Fishing Rod Prefab ve Hand")]
    public GameObject fishingRodPrefab;  // elinde görünmesi için prefab
    public Transform hand;                // Player içindeki Hand objesi
    private GameObject currentRod;

    private void Awake()
    {
        Instance = this;
    }

    public void EquipFishingRod()
    {
        currentTool = ToolType.FishingRod;

        // Elinde prefab spawn et
        if (currentRod != null) Destroy(currentRod);
        if (fishingRodPrefab != null)
        {
            currentRod = Instantiate(fishingRodPrefab, hand);
            currentRod.transform.localPosition = Vector3.zero;
            currentRod.transform.localRotation = Quaternion.identity;
        }
    }

    public void UnequipTool()
    {
        currentTool = ToolType.None;

        // Prefab varsa sil
        if (currentRod != null)
            Destroy(currentRod);
    }

    public bool HasFishingRod()
    {
        return currentTool == ToolType.FishingRod;
    }
}
