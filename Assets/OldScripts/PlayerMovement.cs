using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    //horizontal movement
    [SerializeField]
    private float speed;
    private bool facingRight;

    //vertical movement
    public float jumpForce;
    public float checkRadius;
    [SerializeField]
    private bool isGrounded;
    public LayerMask layerMask;
    [SerializeField]
    private Transform groundCheck;

    public PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Debug.Log("heabfladf");
            //rigidbody2D.velocity = Vector2.up * jumpForce;+
            //rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce) *Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {

        HandleHorizontalMovement();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, layerMask);
    }

    private void HandleHorizontalMovement()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), rigidbody2D.velocity.y);
        rigidbody2D.velocity = movement * speed;

        if (movement.x > 0 && facingRight == false)
        {
            Flip();
        }
        else if(movement.x < 0 && facingRight == true)
        {
            Flip();
        }

        if(movement.x == 0)
        {
            playerState.isRunning = false;
        }
        else
        {
            playerState.isRunning = true;
        }
    }

    private void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
        facingRight = !facingRight;
    }
}
