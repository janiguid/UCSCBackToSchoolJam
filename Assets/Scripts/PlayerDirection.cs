using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerDirection : ScriptableObject, ISerializationCallbackReceiver
{
    public int direction;


    public void OnAfterDeserialize()
    {
        direction = 1;
    }

    public void OnBeforeSerialize()
    {
        
    }
}
