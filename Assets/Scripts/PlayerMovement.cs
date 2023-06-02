using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // private - only this script can interact or make changes to the codes
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    // [SerializeField] is added to the value that we want to be exposed to Unity editor
    // Can directly can the value in the editor to try out different combination etc.

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    // create our own data type with enum
    // a variable that contains several values of movement state
    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;

    /*int wholeNumber = 16;
    float decimalNumber = 4.54f;
    string text = "yadayadayada";
    bool boolean = true;*/

    // Start is called before the first frame update
    private void Start()
    {
        // GetComponent are assigned to be used in our class
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    // Stuff that happens each frame
    private void Update()
    {
        // GetAxisRaw - it turns to 0 almost immediately
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        // GetButtonDown uses Unity input system instead of GetKeyDown
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        // create local variable 
        MovementState state;

        // value is assigned depending on the direction/state of the movement
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        // check the velocity of the rigid body (on the ground or in the air)
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    // prevent player from jumping all the way up up up
    // return a boolean
    // a reference to the box collider
    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 1f, jumpableGround);
    }
}
