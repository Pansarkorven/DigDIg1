using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalViktor : MonoBehaviour
{
    public float speed;
    public float distanceToCollider;
    public LayerMask collisionLayer;

    public Rigidbody2D Rb;
    public Collision2D col;

    private float horizontalInput;
    private CharachterViktor charachter;

    private void Start()
    {
        charachter = GetComponent<CharachterViktor>();
        Rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collision2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("horizontal") != 0)
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }
        else
        {
            horizontalInput = 0;
        }
    }

    private void FixedUpdate()
    {
        Rb.velocity = new Vector2(horizontalInput * speed * Time.deltaTime, Rb.velocity.y);
        SpeedModifier();
    }

    private void SpeedModifier()
    {
        if(!(Rb.velocity.x >= 0 || !charachter.CollisionCheck(Vector2.right, distanceToCollider, collisionLayer)) || (Rb.velocity.x < 0 || charachter.CollisionCheck(Vector2.left, distanceToCollider, collisionLayer)))
        {
          Rb.velocity = new Vector2(.01f, Rb.velocity.y);
        }
    }
}
