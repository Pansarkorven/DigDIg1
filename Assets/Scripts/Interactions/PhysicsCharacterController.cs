using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;


public enum CharacterState // uppräknade lista av karaktär tillstånd
{
    Grounded = 0,
    Airborne = 1,
    Jumping = 2,
    Total

}

public class PhysicsCharacterController : MonoBehaviour
{
    // referens till objektet
    public Rigidbody2D minkropp = null;

    public GameObject myCollisionCheckobject = null;

    public CharacterState JumpingState = CharacterState.Airborne;

    // gravitation
    public float GravityPerSecond = 160.0f; // jag ramlar hjälpp


    // hoppa
    public float JumpSpeedFactor = 3.0f; // Hur snabbare är hoppet
    public float JumpMaxHeight = 150.0f;
    public float JumpHeightDelta = 0.0f;

    // fart
    public float MovementSpeedPerSecond = 10.0f; // farten

    public int HP = 0; // HP fast typ inte
    public List<Sprite> CharacterSprites = new List<Sprite>();
    public SpriteRenderer mySpriteRenderer = null;
    public Vector2 boxSize = new Vector2(0.5f, 2f);





    private void Update()
    {  // om hp är under noll så byts scen till game over
        if(Input.GetKeyDown(KeyCode.E))
            CheckInteraction();



       


        if (Input.GetKeyDown(KeyCode.W) && JumpingState == CharacterState.Grounded)
        {
            JumpingState = CharacterState.Jumping;
            JumpHeightDelta = 0.0f;
        }
    }

    void FixedUpdate()
    {
        Vector3 characterVelocity = minkropp.velocity;
        characterVelocity.x = 0.0f;

        if (JumpingState == CharacterState.Jumping)
        {
            float jumpMovement = MovementSpeedPerSecond * JumpSpeedFactor;
            characterVelocity.y = jumpMovement;

            JumpHeightDelta += jumpMovement * Time.deltaTime;

            if (JumpHeightDelta >= JumpMaxHeight)
            {
                JumpingState = CharacterState.Airborne;

            }
        }

        // vänster
        if (Input.GetKey(KeyCode.A))
        {
            characterVelocity.x -= MovementSpeedPerSecond;
        }
        // höger    
        if (Input.GetKey(KeyCode.D))
        {
            characterVelocity.x += MovementSpeedPerSecond;
        }
        minkropp.velocity = characterVelocity;

    }
    public void TakeDamage(int aHPValue)
    {
        HP += aHPValue;
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

}