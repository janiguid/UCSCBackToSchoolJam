using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{

    [SerializeField]
    private Transform targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(targetPosition);
        Vector3 finalTarget = new Vector3(targetPosition.position.x, targetPosition.position.y, -10);
        transform.position = finalTarget;
    }

    public void SetTarget(Transform newTarget)
    {
        targetPosition = newTarget;
    }
}
