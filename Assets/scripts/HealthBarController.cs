using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;

    private Image fillImage;

    void Start() {
        gameObject.SetActive(true);
    }

    void Awake()
    {
        if (slider.fillRect != null) {
            fillImage = slider.fillRect.GetComponentInChildren<Image>();
        }
    }

    public void SetHealth(int health, int MaxHealth)
    {
        slider.gameObject.SetActive(true);
        slider.maxValue = MaxHealth;
        slider.value = 100 - health;

        Color targetColor = Color.Lerp(High, Low, slider.normalizedValue);
        targetColor.a = 1f;
        fillImage.color = targetColor;
    }

    void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
        transform.position += Offset;
    }
}
