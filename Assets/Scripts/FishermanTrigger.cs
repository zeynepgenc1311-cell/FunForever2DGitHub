using UnityEngine;

public class FishermanTrigger : MonoBehaviour
{
    public GameObject fishermanPanel;
    private bool canOpen = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canOpen)
        {
            fishermanPanel.SetActive(true);
            canOpen = false;
        }
    }

    // bunu CLOSE BUTTON çağıracak
    public void ClosePanel()
    {
        fishermanPanel.SetActive(false);
        canOpen = true;
    }
}
