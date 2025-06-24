using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;

    public AudioSource stepAudio;  
    public AudioSource sfxAudio;   
    public AudioClip jumpClip;     
    public AudioClip attackClip;   

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
            transform.localScale = new Vector3(2f, 2f, 1f); 
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
            transform.localScale = new Vector3(-2f, 2f, 1f); 
        }


        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        animator.SetBool("isRunning", moveInput != 0f);

        if (moveInput != 0f && isGrounded)
        {
            if (!stepAudio.isPlaying)
            {
                stepAudio.Play(); 
            }
        }
        else
        {
            if (stepAudio.isPlaying)
            {
                stepAudio.Stop();  
            }
        }
   
    
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
            animator.SetBool("isJumping", true);

            
            if (jumpClip != null && sfxAudio != null)
            {
                sfxAudio.PlayOneShot(jumpClip, 0.8f);  
            }
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (attackClip != null && sfxAudio != null)
            {
                sfxAudio.PlayOneShot(attackClip, 1f);
            }
        }

        animator.SetBool("isCrouching", Input.GetKey(KeyCode.S));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }
}
