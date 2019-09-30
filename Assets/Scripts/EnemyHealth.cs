using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyHealth : ScriptableObject, ISerializationCallbackReceiver
{
    private float initialHealth = 50;
    public float health;

    public void OnAfterDeserialize()
    {
        health = 50f;
    }

    public void OnBeforeSerialize()
    {
    }
}
