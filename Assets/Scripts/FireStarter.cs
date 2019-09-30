using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStarter : MonoBehaviour
{
    [SerializeField]
    UnityEngine.Experimental.Rendering.LWRP.Light2D fire;

    public void Ignite()
    {
        fire.enabled = true;
    }
}
