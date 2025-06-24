using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    public float attackCooldown = 0.5f;
    private bool canAttack = true;

    public GameObject attackHitbox; 

    void Start()
    {
        animator = GetComponent<Animator>();

        if (attackHitbox != null)
            attackHitbox.SetActive(false);  
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            StartCoroutine(PerformAttack());
        }
    }

    IEnumerator PerformAttack()
    {
        canAttack = false;
        animator.SetTrigger("Attack");

        if (attackHitbox != null)
            attackHitbox.SetActive(true);  

        yield return new WaitForSeconds(0.2f);  

        if (attackHitbox != null)
            attackHitbox.SetActive(false);  

        yield return new WaitForSeconds(attackCooldown - 0.2f);
        canAttack = true;
    }
}
