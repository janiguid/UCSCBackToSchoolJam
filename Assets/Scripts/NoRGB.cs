using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Controller2D))]
public class NoRGB : MonoBehaviour
{

    Controller2D controller;
    public PlayerDirection playerDirection;
    public GameObject projectile;
    public Transform spawnPoint;
    
    float gravity;
    float jumpHeight = 4;
    float timeToReachApex = .4f;
    float jumpVelocity;
    int remainingJumps = 2;

    [SerializeField]
    bool isFairy = false;

    public Vector3 velocity;
    float velocityXSmoothing;
    //float accelerationTimeInAir = .2f;
    //float accelerationTimeGrounded = .1f;
    float accelerationTimeInAir = 0;
    float accelerationTimeGrounded = 0;

    public float moveSpeed = 6;
    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<Controller2D>();

        gravity = -2 * jumpHeight / Mathf.Pow(timeToReachApex, 2);
        jumpVelocity = Mathf.Abs(gravity * timeToReachApex);
        print("Gravity: " + gravity + " Jump Velocity: " + jumpVelocity);

    }

    // Update is called once per frame
    void Update()
    {
        if (controller.collisionInfo.above || controller.collisionInfo.below)
        {
            velocity.y = 0;
            remainingJumps = 2;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


        if (Input.GetKeyDown(KeyCode.UpArrow) && controller.collisionInfo.below)
        {
            velocity.y = jumpVelocity;
        }else if(Input.GetKeyDown(KeyCode.UpArrow) && remainingJumps != 0 && isFairy)
        {
            velocity.y = jumpVelocity;
            --remainingJumps;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
            (controller.collisionInfo.below ? accelerationTimeGrounded : accelerationTimeInAir));


        //Flip sprite
        if(velocity.x > 0 && playerDirection.direction == -1)
        {
            Flip();
            playerDirection.direction = 1;
        }else if (velocity.x < 0 && playerDirection.direction == 1)
        {
            Flip();
            playerDirection.direction = -1;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space))
        {

            SpawnProjectile();
        }
    }

    void SpawnProjectile()
    {
        GameObject proj = Instantiate(projectile);
        proj.transform.position = spawnPoint.position;
        proj.GetComponent<ProjectileHandler>().direction = playerDirection.direction;
    }

    private void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
