using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private int healthLevel = 10;
    private int healthPerLevel = 10;
    private int maxHealth;
    private int currentHealth;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        SetMaxHealth();
        currentHealth = maxHealth;
    }

    private void SetMaxHealth()
    {
        maxHealth = healthLevel * healthPerLevel;
      
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        animator.Play("TakeDamage_01");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.Play("Death_01");
        }
    }
}
