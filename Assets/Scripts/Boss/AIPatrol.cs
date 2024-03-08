using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public Transform[] patrolPoints; // Array of points AI will patrol between
    public float moveSpeed = 3f; // Speed of AI movement
    public float dashSpeed = 8f; // Speed of AI dash
    public float dashDuration = 0.2f; // Duration of dash in seconds
    public KeyCode dashKey = KeyCode.Space; // Key to initiate dash

    private Rigidbody2D rb;
    private int currentPatrolIndex = 0;
    private bool isDashing = false;
    private Vector2 dashDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetNextPatrolPoint();
    }

    void Update()
    {
        if (!isDashing)
        {
            MoveToPatrolPoint();
        }

        if (Input.GetKeyDown(dashKey) && !isDashing)
        {
            Dash();
        }
    }

    void MoveToPatrolPoint()
    {
        Vector2 targetPosition = patrolPoints[currentPatrolIndex].position;
        Vector2 movementDirection = (targetPosition - rb.position).normalized;
        Vector2 movement = movementDirection * moveSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + movement);

        if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
        {
            SetNextPatrolPoint();
        }
    }

    void SetNextPatrolPoint()
    {
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void Dash()
    {
        Vector2 targetPosition = patrolPoints[currentPatrolIndex].position;
        dashDirection = (targetPosition - rb.position).normalized;
        StartCoroutine(PerformDash());
    }

    System.Collections.IEnumerator PerformDash()
    {
        isDashing = true;
        float dashTimer = 0f;

        while (dashTimer < dashDuration)
        {
            rb.velocity = dashDirection * dashSpeed;
            dashTimer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        isDashing = false;
    }
}