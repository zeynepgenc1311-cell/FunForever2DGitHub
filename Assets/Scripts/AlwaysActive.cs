using UnityEngine;

public class AlwaysActive : MonoBehaviour
{
    private void Update()
    {
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
    }
}
