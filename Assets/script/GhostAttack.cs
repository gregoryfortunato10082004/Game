using UnityEngine;

public class GhostAttack : MonoBehaviour
{

    public float detectionRange = 8f;
    public float dashDuration = 0.4f;
    public float returnSpeed = 5f;


    private float attackDamage;
    private float attackCooldown;
    private float moveSpeed;
    private Transform player;
    private Vector2 initialPosition;
    private Vector2 dashDirection;

    private enum State { Waiting, Dashing, Returning, Cooldown }
    private State currentState = State.Waiting;

    private float dashTimer = 0f;
    private float cooldownTimer = 0f;

    private GhostMove ghostMove;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        initialPosition = transform.position;
        ghostMove = GetComponent<GhostMove>();

        attackDamage = GameDifficultyManager.Instance.GetAttackDamage();
        attackCooldown = GameDifficultyManager.Instance.GetAttackCooldown();
        moveSpeed = GameDifficultyManager.Instance.GetMoveSpeed();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Waiting:
                TryStartDash();
                break;

            case State.Dashing:
                DashTowardsPlayer();
                break;

            case State.Returning:
                ReturnToOrigin();
                break;

            case State.Cooldown:
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0f)
                    currentState = State.Waiting;
                break;
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
            scale.x = -Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    void TryStartDash()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            dashDirection = (player.position - transform.position).normalized;
            dashTimer = dashDuration;
            currentState = State.Dashing;
            initialPosition = transform.position;

            if (ghostMove != null)
                ghostMove.SetFloatingActive(false);
        }
    }

    void DashTowardsPlayer()
    {
        transform.position += (Vector3)(dashDirection * moveSpeed * Time.deltaTime);
        dashTimer -= Time.deltaTime;

        if (dashTimer <= 0f)
        {
            currentState = State.Returning;
        }
    }

    void ReturnToOrigin()
    {
        Vector2 currentPosition = transform.position;
        Vector2 directionToOrigin = (initialPosition - currentPosition).normalized;
        float distance = Vector2.Distance(currentPosition, initialPosition);

        if (distance > 0.05f)
        {
            transform.position += (Vector3)(directionToOrigin * returnSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = initialPosition;
            cooldownTimer = attackCooldown;
            currentState = State.Cooldown;
            if (ghostMove != null)
                ghostMove.SetFloatingActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colidiu com o player - dano tentado");
        if (currentState == State.Dashing && other.CompareTag("Player"))
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage((int)attackDamage);
            }

            cooldownTimer = attackCooldown;
            currentState = State.Cooldown;
            
            if (ghostMove != null)
                ghostMove.SetFloatingActive(true);
        }
    }
}
