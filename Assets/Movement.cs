using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rb;

    public float speed;
    private float startingSpeed;
    public float shieldSpeed;
    public float jumpForce;
    private float startingJumpForce;
    public float moveInput;

    private int extraJumps;
    public int extraJumpsValue;

    public Animator animator;

    public bool Turned = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        animator.SetBool("IsJumping", !isGrounded);

        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        animator.SetFloat("FallingSpeed", (rb.velocity.y));
        if (rb.velocity.y < -0.1f)
        {
            animator.SetBool("IsFalling", true);
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsFalling", false);

        }
    }

    private void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
            animator.SetBool("IsJumping", true);

        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
       
        if (Input.GetAxis("Horizontal") < 0 && !Turned)
        {
            Flip();
        }

        if (Input.GetAxis("Horizontal") > 0 && Turned)
        {
            Flip();
        }
    }

    void Flip()
    {
        Turned = !Turned;

        transform.Rotate(0f, 180f, 0f);
    }

}