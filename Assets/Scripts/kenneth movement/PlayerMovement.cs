using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private bool isRunning = false; // indikation ifall om spelaren springer
    private float runningSpeed = 12f; // farten när spelaren springer
    private float walkingSpeed = 8f; // farten när spelaren går vanligt
    private float jumpingPower = 16f; // hur långt han hoppar
    private bool isFacingRight = true; // indikation om vilket håll spelaren är åt
    public Vector2 boxSize = new Vector2(0.5f, 2f); // storlek på interaktion avståndet för objekt

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // växla mellan att springa och att gå
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.E)) // ifall spelaren använder något
        {
            CheckInteraction();
        }

        Flip();
    }

    private void FixedUpdate()
    {
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

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}