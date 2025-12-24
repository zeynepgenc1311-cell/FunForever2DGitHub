using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MiniGameController : MonoBehaviour
{
    public static MiniGameController Instance;

    [Header("Slider")]
    public Slider slider;

    [Header("Target Zone (balığın çıkacağı alan)")]
    public RectTransform targetZone;

    [Header("Hız")]
    public float speed = 2f;

    [HideInInspector] public bool finished;
    [HideInInspector] public bool success;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartMiniGame()
    {
        finished = false;
        success = false;
        slider.value = 0;
        StartCoroutine(MoveSlider());
    }

    IEnumerator MoveSlider()
    {
        while (!finished)
        {
            slider.value = Mathf.PingPong(Time.time * speed, 1f);

            // Space tuşuna basınca kontrol
            if (Input.GetKeyDown(KeyCode.Space))
            {
                float sliderPos = slider.value;
                float min = targetZone.anchorMin.x;
                float max = targetZone.anchorMax.x;

                success = sliderPos >= min && sliderPos <= max;
                finished = true;
            }

            yield return null;
        }
    }
}
