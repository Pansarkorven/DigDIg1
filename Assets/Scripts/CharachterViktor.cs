using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterViktor : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Collider2D Col;
    protected CharachterViktor charachter;

    public bool isGrounded;
    public bool isJumping;

    // Update is called once per frame
    void Start()
    {
        Initializtion();
    }

    protected virtual void Initializtion()
    {
        charachter = GetComponent<CharachterViktor>();
        rb = GetComponent<Rigidbody2D>();
        Col = GetComponent<Collider2D>();
    }

    public virtual bool CollisionCheck(Vector2 direction, float distance, LayerMask collision)
    {
        RaycastHit2D[] hits = new RaycastHit2D[10];
        int numHits = Col.Cast(direction, hits, distance);
        for (int i = 0; i < numHits; i++)
        {
            if ((1 << hits[i].collider.gameObject.layer & collision) != 0)
            {
                return true;
            }
        }
        return false;
    }
}

