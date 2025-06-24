using UnityEngine;

public class HitBoxAttack : MonoBehaviour
{
    public int attackDamage = 20;
    public string targetTag = "Enemy"; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            HealthSystem targetHealth = other.GetComponent<HealthSystem>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(attackDamage);
                Debug.Log($"[HitBox] Dano causado a {targetTag}: {attackDamage}");
            }
        }
    }
}
