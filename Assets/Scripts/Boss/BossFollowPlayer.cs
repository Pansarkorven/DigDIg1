using System.Collections;
using UnityEngine;

public class BossFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float jumpHeightThreshold = 2f;
    [SerializeField] float jumpCooldown = 2f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Animator Anim;

    [SerializeField] float lastJumpTime;
    [SerializeField] float lastPlayerAboveTime;
    [SerializeField] float playerAboveCooldown = 2f;

    bool canMove = true;
    bool canMoveCloseEnugh;
    bool BossFacingRight = false;

    [SerializeField] float DistanceThreshold = 5.0f;

    void Update()
    {
        if (canMove && canMoveCloseEnugh)
        {
            MoveTowardsPlayer();
            Anim.SetBool("BossMoving", true);
        }
        else
        {
            Anim.SetBool("BossMoving", false);
        }
        if (canMove)
        {
            // Assuming you have some input to determine horizontal movement
            float horizontal = Input.GetAxisRaw("Horizontal");
            Flip();
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (canMove)
        {
            if (distance < DistanceThreshold)
            {
                canMoveCloseEnugh = false;
            }
            else
            {
                canMoveCloseEnugh = true;
            }
        }
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            float verticalDistance = player.position.y - transform.position.y;
            if (verticalDistance > 3.85 && verticalDistance < jumpHeightThreshold && Time.time - lastJumpTime > jumpCooldown)
            {
                if (Time.time - lastPlayerAboveTime > playerAboveCooldown)
                {
                    Jump();
                    lastPlayerAboveTime = Time.time;
                }
            }
        }
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, jumpForce);
        lastJumpTime = Time.time;
    }

    public void StopMoving()
    {
        canMove = false;
    }

    public void StartMoving()
    {
        canMove = true;
    }

    private void Flip()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 direction = player.position - transform.position;
            // Flip the sprite based on the direction
            if (direction.x > 0 && !BossFacingRight || direction.x < 0 && BossFacingRight)
            {
                BossFacingRight = !BossFacingRight;
                Debug.Log("Boss FLip");

                // Flip the sprite directly
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
    }
}