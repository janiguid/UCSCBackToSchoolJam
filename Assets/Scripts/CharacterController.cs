using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private float PlayerXBound;
    [SerializeField]
    private float PlayerYBound;

    [SerializeField]
    private Rigidbody2D rigidbody2D;
    [SerializeField]
    private CharacterController partner;

    [SerializeField]
    private bool isMoving;
    [SerializeField]
    private float speed;

    Vector2 movement;
    Vector2 finalPosition;


    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if(partner == null)
        {
            print("Failed to assign partner");
            return;
        }

        if (isMoving)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement = Vector2.zero;
        }

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

    }


    //We're doing the physics in Fixed Update because Update depends on frame rate
    //and might cause physics calculations to act weirdly
    private void FixedUpdate()
    {
        finalPosition = rigidbody2D.position + movement* speed * Time.fixedDeltaTime;
        ClampMovement();
        //we multiply movement by Time.fixedDeltaTime to make calculations constant
        rigidbody2D.MovePosition(finalPosition);
    }


    private void ClampMovement()
    {
        finalPosition.x = Mathf.Clamp(finalPosition.x, -PlayerXBound, PlayerXBound);
        finalPosition.y = Mathf.Clamp(finalPosition.y, -PlayerYBound, PlayerYBound);
    }

    public bool GetMoveState()
    {
        return isMoving;
    }

    public void SetMoveState(bool newState)
    {
        isMoving = newState;
    }
}
