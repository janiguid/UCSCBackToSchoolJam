using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public static ControlManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private CharacterController P1;
    [SerializeField]
    private CharacterController P2;

    [SerializeField]
    private SimpleCameraController cameraTarget;

    public bool playerMovesWithLight;

    private void Start()
    {
        //Uncomment this for the intended gameplay
        //Game starts with camera focusing on P1
        //while P2 is moving
        //P1.SetMoveState(false);
        //P2.SetMoveState(true);
        //cameraTarget.SetTarget(P1.transform);

        //Code below is just for testing purposes
        P1.SetMoveState(true);
        P2.SetMoveState(false);
        cameraTarget.SetTarget(P1.transform);
    }

    // Update is called once per frame
    void Update()
    {
        //Uncomment this for testing design
        //if (playerMovesWithLight)
        //{
        //    //prototype version
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        if (P1.GetMoveState() == true && P2.GetMoveState() == false)
        //        {
        //            P1.SetMoveState(false);
        //            P2.SetMoveState(true);
        //            cameraTarget.SetTarget(P2.transform);
        //        }
        //        else if (P2.GetMoveState() == true && P1.GetMoveState() == false)
        //        {
        //            P2.SetMoveState(false);
        //            P1.SetMoveState(true);
        //            cameraTarget.SetTarget(P1.transform);
        //        }
        //    }
        //}
        //else
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        if (P1.GetMoveState() == true && P2.GetMoveState() == false)
        //        {
        //            P1.SetMoveState(false);
        //            P2.SetMoveState(true);
        //            cameraTarget.SetTarget(P1.transform);
        //        }
        //        else if (P2.GetMoveState() == true && P1.GetMoveState() == false)
        //        {
        //            P2.SetMoveState(false);
        //            P1.SetMoveState(true);
        //            cameraTarget.SetTarget(P2.transform);
        //        }
        //    }
        //}



        //prototype version
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (P1.GetMoveState() == true && P2.GetMoveState() == false)
            {
                P1.SetMoveState(false);
                P2.SetMoveState(true);
                cameraTarget.SetTarget(P2.transform);
            }
            else if (P2.GetMoveState() == true && P1.GetMoveState() == false)
            {
                P2.SetMoveState(false);
                P1.SetMoveState(true);
                cameraTarget.SetTarget(P1.transform);
            }
        }
    }
}
