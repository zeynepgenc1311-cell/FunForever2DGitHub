using UnityEngine;

public class MiniGameUIFollowPlayer : MonoBehaviour
{
    public Transform player;   // player objesi
    public Vector3 offset;     // panelin player üstünde ne kadar yukarıda duracağı
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (player != null && gameObject.activeSelf)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(player.position + offset);
            rect.position = screenPos;
        }
    }
}
