using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private float sliderAnimationDuration = 0.2f;
    private bool isAnimating = false;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    public void SetMaxHealthOnUI(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth; 
    }

    public async void SetCurrentHealthOnUI(int health)
    {
        if (isAnimating) return;

        isAnimating = true;

        float startValue = slider.value;
        float elapsed = 0f;

        while (elapsed < sliderAnimationDuration) 
        {

            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / sliderAnimationDuration);
            slider.value = Mathf.Lerp(startValue, health, t);

            await Task.Yield();
        }

        slider.value = health;
        isAnimating = false;
    }
}
