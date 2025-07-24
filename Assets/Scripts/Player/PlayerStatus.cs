using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private int healthLevel = 20;
    private int healthPerLevel = 4;
    private int maxHealth;
    private int currentHealth;
    private AnimationManager animationManager;

    public HealthBar healthBar;

    private void Awake()
    {
        animationManager = GetComponentInChildren<AnimationManager>();
    }
    void Start()
    {
        SetMaxHealth();
        currentHealth = maxHealth;
    }

    private void SetMaxHealth()
    {
        maxHealth = healthLevel * healthPerLevel;
        healthBar.SetMaxHealthOnUI(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthBar.SetCurrentHealthOnUI(currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animationManager.playAnimation(AnimationKeys.animations[AnimationsEnum.death1], true);
        }
        else
            animationManager.playAnimation(AnimationKeys.animations[AnimationsEnum.damage1], true);
    }
}
