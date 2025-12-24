using UnityEngine;

public class PlayerFishing : MonoBehaviour
{
    public Animator animator;

    private bool canFish;
    private bool isFishing;

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
        if (canFish && !isFishing && Input.GetKeyDown(KeyCode.E))
        {
            isFishing = true;
            animator.SetTrigger("Cast"); // animasyon yok ama trigger hazır
            FishingManager.Instance.CastRod();
        }
    }

    public void SetIdleFishing()
    {
        animator.SetBool("IsFishing", true);
    }

    public void StopFishing()
    {
        animator.SetBool("IsFishing", false);
        isFishing = false;
    }

    public void ReelSuccess()
    {
        animator.SetTrigger("ReelSuccess");
    }

    public void ReelFail()
    {
        animator.SetTrigger("ReelFail");
    }
}
