using UnityEngine;

public class HurtAnimationController : MonoBehaviour
{
    private Animator animator;
    private HealthSystem healthSystem;

    private void Start()
    {
        animator = GetComponent<Animator>();
        healthSystem = GetComponent<HealthSystem>();

       
        if (healthSystem != null)
        {
            healthSystem.onDamageTaken += PlayHurtAnimation;
        }
    }

    private void PlayHurtAnimation()
    {
        animator.SetBool("isHurt", true);   
    }

    
    public void ResetHurtState()
    {
        animator.SetBool("isHurt", false);
    }

    private void OnDestroy()
    {
    
        if (healthSystem != null)
        {
            healthSystem.onDamageTaken -= PlayHurtAnimation;
        }
    }
}
