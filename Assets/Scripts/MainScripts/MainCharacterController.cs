using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MainCharacterController : MonoBehaviour
{
    [SerializeField] float horizontal;
    public bool isRunning = false; // Flag to indicate if the player is running
    [SerializeField] float runningSpeed = 12f; // Speed when running
    [SerializeField] float walkingSpeed = 8f; // Speed when walking
    [SerializeField] float jumpingPower = 16f;
    bool isFacingRight = true;
    [SerializeField] Vector2 boxSize = new Vector2(0.5f, 2f);
    [SerializeField] float flipDistance = 0.1f;

    [SerializeField] AudioClip[] footstepSounds;
    [SerializeField] AudioSource footstepAudioSource;

    public bool canDash = false;
    bool isDashing;
    int currentFootstepIndex = 0;


    public bool CanMove;
    [SerializeField] public bool IsAttack;

    [SerializeField] float dashingPower = 4f;
    [SerializeField] float dashingTime = 0.1f;
    [SerializeField] float dashingCooldown = 7f;

    [SerializeField] Animator Anim;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] float CoyoteTime = 0.2f;
    [SerializeField] float CoyoteTimeCounter;

    

    // Update is called once per frame

    private void Start()
    {
        Anim = GetComponent<Animator>();
        AllowsMove();
        footstepAudioSource = GetComponent<AudioSource>();
        if (footstepAudioSource == null)
        {
        }

        //bool value = AtC.isAttacking;
    }

   //private void PlayFootstepSound()
   // {
   //     if (!footstepAudioSource.isPlaying)
   //     {
   //         footstepAudioSource.clip = footstepSounds[currentFootstepIndex];
   //         footstepAudioSource.Play();
   //         currentFootstepIndex = (currentFootstepIndex + 1) % footstepSounds.Length;
   //     }
   // }

    void PlayFootstepSound()
    {
        if (!footstepAudioSource.isPlaying)
        {
            int randomIndex = Random.Range(0, footstepSounds.Length);
            footstepAudioSource.clip = footstepSounds[randomIndex];
            footstepAudioSource.Play();
        }
    }

    public void StopFootstepSound()
    {
        footstepAudioSource.Stop();
    }
    void Update()
    {
        if (IsAttack == false)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        if (isDashing)
        {
            return;
        }
        if (horizontal != 0 && IsGrounded() && !isDashing)
        {
            PlayFootstepSound();
        }

        if (IsGrounded())
        {
            CoyoteTimeCounter = CoyoteTime;
        }
        else
        {
            CoyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && horizontal != 0)
        {
            isRunning = true;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) || horizontal == 0)
        {
            isRunning = false;
        }

        if (Input.GetButtonDown("Jump") && CoyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            Anim.SetTrigger("Jump");
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            CoyoteTimeCounter = 0f;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckInteraction();
        }

        if (horizontal != 0)
        {
            Anim.SetBool("IsRunning", true);
            
        }
        else
        {
            Anim.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && canDash)
        {
            StartCoroutine(Dash());
            Anim.SetTrigger("Dash");
        }

        Flip();


    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        // Calculate movement speed
        float currentSpeed = isRunning ? runningSpeed : walkingSpeed;
        rb.velocity = new Vector2(horizontal * currentSpeed, rb.velocity.y);
    }

    private void CheckInteraction()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, boxSize, 0);

        foreach (Collider2D collider in colliders)
        {
            Interaction interactionComponent = collider.GetComponent<Interaction>();

            if (interactionComponent != null)
            {
                interactionComponent.Interact();
                return;
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    
    public void AllowsMove()
    {
        IsAttack = false;
        
    }

    public void StopMove()
    {
        
        IsAttack = true; 
        horizontal = 0;
    }

   

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;

            // Store the current position
            Vector3 currentPosition = transform.position;

            // Flip the scale
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

            // Adjust the position to reflect the flip
            // Adjust this value based on how much you want the character to move when flipping
            transform.position = new Vector3(currentPosition.x + (isFacingRight ? flipDistance : -flipDistance), currentPosition.y, currentPosition.z);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }

    //private void Flip()
    //{
    //    if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
    //    {
    //        isFacingRight = !isFacingRight;
    //        Vector3 localScale = transform.localScale;
    //        localScale.x *= -1f;
    //        transform.localScale = localScale;
    //    }
    //}
}