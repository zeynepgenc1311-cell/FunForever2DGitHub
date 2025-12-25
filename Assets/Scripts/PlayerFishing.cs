using UnityEngine;

public class PlayerFishing : MonoBehaviour
{
    public Animator animator;

    private bool canFish;
    private bool isFishing;

    public static PlayerFishing Instance;

private void Awake()
{
    Instance = this;
}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
            canFish = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
            canFish = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TryStartFishing();
    }

    // 🔥 HEM BUTON HEM KLAVYE BURAYI KULLANIR
    public void TryStartFishing()
    {
        if (!canFish) return;
        if (isFishing) return;

        isFishing = true;

        if (animator != null)
            animator.SetTrigger("Cast");

        FishingManager.Instance.OnFishingButton();
    }

    public void SetIdleFishing()
    {
        if (animator != null)
            animator.SetBool("IsFishing", true);
    }

    public void StopFishing()
    {
        if (animator != null)
            animator.SetBool("IsFishing", false);

        isFishing = false;
    }

    public void ReelSuccess()
    {
        if (animator != null)
            animator.SetTrigger("ReelSuccess");
    }

    public void ReelFail()
    {
        if (animator != null)
            animator.SetTrigger("ReelFail");
    }

    public bool CanFish()
{
    return canFish;
}

}
