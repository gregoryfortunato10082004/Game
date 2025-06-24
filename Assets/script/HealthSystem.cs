using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public delegate void OnDamageTaken();
    public event OnDamageTaken onDamageTaken;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        Debug.Log("Dano REAL aplicado: " + damageAmount);
        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(0, currentHealth);
        
        Debug.Log($"[HealthSystem] Dano recebido: {damageAmount}, Vida atual: {currentHealth}");
        onDamageTaken?.Invoke();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " morreu!");
        Destroy(gameObject, 0.5f);
    }
}
