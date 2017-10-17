using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manny : MonoBehaviour {

    public float speed = 5f;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator animator;

    public bool facingRight = true;

    public float jumpSpeed =5f;

    bool isJumping = false;

    private float rayCastLength = 0.005f;
    private float width;
    private float height;

    private float jumpButtonPressTime;

    private float maxJumpTime = 0.2f;

     void FixedUpdate()
    {
        // Get horizontal movement -1 Left, or 1 Right
        float horzMove = Input.GetAxisRaw("Horizontal");

        // Need to get Mannys y
        Vector2 vect = rb.velocity;

        // Change x and keep y as is
        rb.velocity = new Vector2(horzMove * speed, vect.y);

        // Set the speed so the right Animation is played
        animator.SetFloat("Speed", Mathf.Abs(horzMove));

        if (horzMove > 0 && !facingRight)
        {
            FlipManny();
        }
        else if (horzMove < 0 && facingRight) {
            FlipManny();
        }

        float vertMove = Input.GetAxis("Jump");

        if (IsOnGround() && isJumping == false)
        {
            if (vertMove > 0f)
            {
                isJumping = true;
            }
        }

        if (jumpButtonPressTime > maxJumpTime) {
            vertMove = 0f;
        }
        // If is jumping and we have a valid jump press length
        // make Manny jump
        if (isJumping && (jumpButtonPressTime < maxJumpTime))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        // If we have moved high enough make Manny fall
        // Set Mannys Rigidbody 2d Gravity Scale to 2
        if (vertMove >= 1f)
        {
            jumpButtonPressTime += Time.deltaTime;
        }
        else
        {
            isJumping = false;
            jumpButtonPressTime = 0f;
        }

    }

    public bool IsOnGround()
    {

        // Check if contacting the ground straight down
        bool groundCheck1 = Physics2D.Raycast(new Vector2(
                                transform.position.x,
                                transform.position.y - height),
                                -Vector2.up, rayCastLength);

        // Check if contacting ground to the right
        bool groundCheck2 = Physics2D.Raycast(new Vector2(
            transform.position.x + (width - 0.2f),
            transform.position.y - height),
            -Vector2.up, rayCastLength);

        // Check if contacting ground to the left
        bool groundCheck3 = Physics2D.Raycast(new Vector2(
            transform.position.x - (width - 0.2f),
            transform.position.y - height),
            -Vector2.up, rayCastLength);

        if (groundCheck1 || groundCheck2 || groundCheck3)
            return true;

        return false;

    }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // Gets Mannys collider width and height and
        // then adds more to it. Used to raycast to see
        // if Manny is colliding with anything so we
        // can jump
        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.2f;
    }
    
    void FlipManny()
    {

        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
  
   
    // If Manny falls off the screen destroy the game object
    /*
	void OnBecameInvisible(){
		Debug.Log ("Manny Destroyed");
		Destroy (gameObject);
	}
	*/
}
