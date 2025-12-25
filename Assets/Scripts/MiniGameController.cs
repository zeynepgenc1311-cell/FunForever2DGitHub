using UnityEngine;
using UnityEngine.UI;

public class MiniGameController : MonoBehaviour
{
    public static MiniGameController Instance;

    [Header("Slider")]
    public Slider slider;

    [Header("Target Zone")]
    public RectTransform targetZone;

    [Header("Progress Bar")]
    public Image progressBar;

    [Header("Ayarlar")]
    public float sliderUpSpeed = 0.6f;
    public float sliderDownSpeed = 0.6f;

    public float targetMoveSpeed01 = 1.2f; // target zone hızı
    public float targetZoneSize = 0.2f;    // genişliği (0–1 arası)

    public float progressFillSpeed = 0.4f;
    public float progressLoseSpeed = 0.3f;

    private bool holdingButton;
    public bool finished;
    public bool success;

    float targetCenter01;

    private void Awake()
    {
        Instance = this;
    }

    public void StartMiniGame()
    {
        finished = false;
        success = false;

        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = 0f;

        progressBar.fillAmount = 0f;
        holdingButton = false;
    }

    void Update()
    {
        if (finished) return;

        MoveTargetZone();
        MoveSlider();
        CheckProgress();
    }

    // 🎯 SLIDER BUTONLA İLERLER GERİ GELİR
    void MoveSlider()
    {
        if (holdingButton)
            slider.value += sliderUpSpeed * Time.deltaTime;
        else
            slider.value -= sliderDownSpeed * Time.deltaTime;

        slider.value = Mathf.Clamp01(slider.value);
    }

    // 🎯 TARGET ZONE 0–1 ARASI SAĞA SOLA GİDER
    void MoveTargetZone()
    {
        targetCenter01 = (Mathf.Sin(Time.time * targetMoveSpeed01) + 1f) / 2f;

        float sliderWidth = slider.fillRect.rect.width;

        float xPos = Mathf.Lerp(
            -sliderWidth / 2f,
            sliderWidth / 2f,
            targetCenter01
        );

        targetZone.anchoredPosition = new Vector2(
            xPos,
            targetZone.anchoredPosition.y
        );
    }

    // 🎯 PROGRESS DOLMA KONTROLÜ
    void CheckProgress()
{
    float distance = Mathf.Abs(slider.value - targetCenter01);

    float perfectZone = targetZoneSize * 0.4f;
    float safeZone = targetZoneSize * 1.2f;

    if (distance <= perfectZone)
    {
        // hedefin içi → hızlı dol
        progressBar.fillAmount += progressFillSpeed * Time.deltaTime;
    }
    else if (distance <= safeZone)
    {
        // hedefe yakın → BAR SABİT
        // hiçbir şey yapma
    }
    else
    {
        // çok kaçtı → yavaş azalsın
        progressBar.fillAmount -= progressLoseSpeed * 0.2f * Time.deltaTime;
    }

    progressBar.fillAmount = Mathf.Clamp01(progressBar.fillAmount);

    if (progressBar.fillAmount >= 1f)
    {
        success = true;
        finished = true;
    }
}



    // 🎯 BUTON EVENTLERİ
    public void ButtonDown()
    {
        holdingButton = true;
    }

    public void ButtonUp()
    {
        holdingButton = false;
    }
}
