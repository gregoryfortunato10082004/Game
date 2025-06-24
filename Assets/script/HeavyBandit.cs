using UnityEngine;

public class HeavyBandit : MonoBehaviour
{
    public Transform player;
    public float visionRange = 8f;
    public float attackRange = 1.5f;
    
    private float attackDamage;
    private float attackCooldown;
    private float moveSpeed;
    private Animator animator;
    private Rigidbody2D rb;
    private HealthSystem health;
    private float attackTimer = 0f;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<HealthSystem>();
        health.onDamageTaken += OnHurt;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Debug.Log("HeavyBandit Start - Dificuldade atual: " + GameDifficultyManager.Instance.currentDifficulty);
        
        attackDamage = GameDifficultyManager.Instance.GetAttackDamage();
        attackCooldown = GameDifficultyManager.Instance.GetAttackCooldown();
        moveSpeed = GameDifficultyManager.Instance.GetMoveSpeed();
        
        Debug.Log($"HeavyBandit configurado - Damage: {attackDamage}, Cooldown: {attackCooldown}, Speed: {moveSpeed}");
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        attackTimer -= Time.deltaTime;

        if (distance <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isRunning", false);

            if (attackTimer <= 0f)
            {
                animator.SetTrigger("Attack");
                attackTimer = attackCooldown;
            }
        }
        else if (distance <= visionRange)
        {
            Vector2 direction = new Vector2(player.position.x - transform.position.x, 0f).normalized;
            rb.linearVelocity = direction * moveSpeed;
            animator.SetBool("isRunning", true);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isRunning", false);
        }

        UpdateLookDirection();
    }

    void UpdateLookDirection()
    {   
        if (player == null)
            return;

        Vector3 scale = transform.localScale;

        if (player.position.x < transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }

    void OnHurt()
    {
        animator.SetTrigger("Hurt");
    }

    public void AttackPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            var playerHealth = player.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                Debug.Log($"HeavyBandit atacando com damage: {attackDamage}");
                playerHealth.TakeDamage((int)attackDamage);
            }
        }
    }

    public void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("Death");
    }
}