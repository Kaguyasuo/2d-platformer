using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Represents components of the player
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    private SpriteRenderer playerRenderer;
    private BoxCollider2D playerCollider;

    // factor for jump and run strength
    [SerializeField] private float jumpStrength = 13f;
    [SerializeField] private float runStrength = 10f;

    // stores the direction the player is holding towards on their controller
    private float dirX = 0f;

    // the radius at the player's feet which checks if the player is grounded
    // [SerializeField] private float checkRadius = 0.3f;

    // layerMask for what is ground
    [SerializeField] private LayerMask whatIsGround;

    // stores boolean of if the player is grounded
    // private bool isGrounded = false;

    // stores the Transform object of the feet object
    // public Transform feetPosition;

    // amount of time in which player can hold down jump button to jump higher
    [SerializeField] private float jumpTime = 0.25f;

    // float for the amount of time the player has jumped in a single jump. and a boolean for if the player is jumping or not
    private float jumpTimeCounter;
    private bool isJumping;

    private enum MovementState {idle, running, jumping, falling}

    // Start is called before the first frame update
    private void Start()
    {
        // Get RigidBody, Animator, and SpriteRenderer
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<BoxCollider2D>();

    }

    // Update is called based on the computer frame rate
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        // isGrounded = Physics2D.OverlapCircle(feetPosition.position, checkRadius, whatIsGround);
        playerRigidBody.velocity = new Vector2(dirX * runStrength, playerRigidBody.velocity.y);


        // Player jump check
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpStrength);
        }
        if (Input.GetKey(KeyCode.Space) && isJumping)
        { 
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpStrength);

            if (jumpTimeCounter > 0)
            {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpStrength);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        UpdateAnimationState();


    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            playerRenderer.flipX = false;
        }
        else if (dirX < 0)
        {
            state = MovementState.running;
            playerRenderer.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (playerRigidBody.velocity.y > .1f && !IsGrounded())
        {
            state = MovementState.jumping;
        }
        else if (playerRigidBody.velocity.y < 0.1f && !IsGrounded())
        {
            state = MovementState.falling;
        }

        playerAnimator.SetInteger("state", (int) state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, 0.1f, whatIsGround);
    }
}
