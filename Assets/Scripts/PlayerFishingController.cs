using UnityEngine;

public class PlayerFishingController : MonoBehaviour
{
    public GameObject fishingButton;
    public Player player;

    void Update()
    {
        if (player == null || fishingButton == null) return;

        if (player.HasFishingRod())
            fishingButton.SetActive(true);
        else
            fishingButton.SetActive(false);
    }
}
