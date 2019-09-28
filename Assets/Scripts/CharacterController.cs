using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbody2D;
    [SerializeField]
    private CharacterController partner;

    [SerializeField]
    private bool isMoving;
    [SerializeField]
    private float speed;

    Vector2 movement;

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

    }


    //We're doing the physics in Fixed Update because Update depends on frame rate
    //and might cause physics calculations to act weirdly
    private void FixedUpdate()
    {
        //we multiply movement by Time.fixedDeltaTime to make calculations constant
        rigidbody2D.MovePosition(rigidbody2D.position + movement * speed * Time.fixedDeltaTime);
    }

    //void SwitchControl()
    //{
    //    //Make partner move
    //    partner.ActivateMovement();

    //    //Stop me from moving
    //    DeactivateMovement();


    //}

    //public void ActivateMovement()
    //{
    //    isMoving = true;
    //}

    //void DeactivateMovement()
    //{
    //    isMoving = false;
    //}

    public bool GetMoveState()
    {
        return isMoving;
    }

    public void SetMoveState(bool newState)
    {
        isMoving = newState;
    }
}
