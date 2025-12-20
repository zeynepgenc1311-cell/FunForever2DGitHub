using UnityEngine;

public class FishermanTrigger : MonoBehaviour
{
    public GameObject fishermanPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            fishermanPanel.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            fishermanPanel.SetActive(false);
    }
}
