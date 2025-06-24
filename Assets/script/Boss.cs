using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public float visionRange = 10f;
    public float attackRange = 2.5f;
    public float specialAttackCooldown = 10f;

    public GameObject attackHitbox;

    private float attackDamage;
    private float attackCooldown;
    private float moveSpeed;

    private float specialCooldownTimer = 0f;
    private Animator animator;
    private Rigidbody2D rb;
    private HealthSystem health;
    private float cooldownTimer = 0f;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<HealthSystem>();
        health.onDamageTaken += OnHurt;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        attackDamage = GameDifficultyManager.Instance.GetAttackDamage();
        attackCooldown = GameDifficultyManager.Instance.GetAttackCooldown();
        moveSpeed = GameDifficultyManager.Instance.GetMoveSpeed();
    }

    void Update()
    {
        if (isDead || player == null) return;


        Vector2 bossPos = new Vector2(transform.position.x, 0f);
        Vector2 playerPos = new Vector2(player.position.x, 0f);
        float distance = Vector2.Distance(bossPos, playerPos);

        cooldownTimer -= Time.deltaTime;

        if (distance <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isWalking", false);

            if (cooldownTimer <= 0f)
            {
                animator.SetTrigger("Attack");
                cooldownTimer = attackCooldown;
            }
        }
        
        else if (distance <= visionRange)
        {
            Vector2 direction = new Vector2(player.position.x - transform.position.x, 0f).normalized;
            rb.linearVelocity = direction * moveSpeed;
            animator.SetBool("isWalking", true);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isWalking", false);
        }

        specialCooldownTimer -= Time.deltaTime;

        if (distance <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isWalking", false);

            if (cooldownTimer <= 0f)
            {
                
                if (specialCooldownTimer <= 0f)
                {
                    animator.SetTrigger("Cast"); 
                    specialCooldownTimer = specialAttackCooldown;
                }
                else
                {
                    animator.SetTrigger("Attack");
                }

                cooldownTimer = attackCooldown;
            }
        }

        UpdateLookDirection();
    }

    void UpdateLookDirection()
    {
        if (player == null) return;

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
            Debug.Log($"[HealthSystem] Dano recebido do boss:");
            if (playerHealth != null)
                playerHealth.TakeDamage((int)attackDamage);
        }
    }

    public void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("isDeath");
    }
}
