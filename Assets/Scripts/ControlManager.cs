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


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(P1.GetMoveState() == true && P2.GetMoveState() == false)
            {
                P1.SetMoveState(false);
                P2.SetMoveState(true);
                cameraTarget.SetTarget(P2.transform);
            }else if(P2.GetMoveState() == true && P1.GetMoveState() == false)
            {
                P2.SetMoveState(false);
                P1.SetMoveState(true);
                cameraTarget.SetTarget(P1.transform);
            }
        }
    }
}
