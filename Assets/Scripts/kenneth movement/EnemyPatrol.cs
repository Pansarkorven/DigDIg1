using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;

    public float moveSpeed = 3f; // Speed of AI movement
    public float dashSpeed = 8f; // Speed of AI dash
    public float dashDuration = 0.2f; // Duration of dash in seconds
    public float dashCooldown = 2f; // Cooldown for the dash ability
    public KeyCode dashKey = KeyCode.Space;

    private bool dash;

    private bool isDashing = false;
    private bool dashCooldownActive = false;
    private Vector2 dashDirection;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }


        if (Input.GetKeyDown(dashKey) && !isDashing && !dashCooldownActive)
        {
            Dash();
        }


    }

    void Dash()
    {
        dashDirection = ((Vector2)currentPoint.position - rb.position).normalized;
        StartCoroutine(PerformDash());
        StartCoroutine(DashCooldown());
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

    System.Collections.IEnumerator DashCooldown()
    {
        dashCooldownActive = true;
        yield return new WaitForSeconds(dashCooldown);
        dashCooldownActive = false;
    }
}
