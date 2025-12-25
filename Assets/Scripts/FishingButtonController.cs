using UnityEngine;

public class FishingButtonController : MonoBehaviour
{
    public Player player;

    void Update()
    {
        if (player.HasFishingRod())
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

    public void OnFishingButtonPressed()
    {
        FishingManager.Instance.OnFishingButton();
    }
}
