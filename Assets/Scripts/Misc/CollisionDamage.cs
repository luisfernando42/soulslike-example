using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    Collider damageCollider;
    public int damage = 25;

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }

    public void EnableCollider() { damageCollider.enabled = true; }

    public void DisableCollider() { damageCollider.enabled = false; }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            PlayerStatus player = col.GetComponent<PlayerStatus>();

            if(player != null )
            {
                player.TakeDamage(damage);
            }
        }

        if (col.tag == "Enemy")
        {
            EnemyStatus enemy = col.GetComponent<EnemyStatus>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
