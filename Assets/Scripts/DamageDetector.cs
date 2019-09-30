using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    [SerializeField]
    private EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float damage)
    {
        if (enemyHealth.health - damage < 0)
        {
            enemyHealth.health = 0;
            return;
        }
        enemyHealth.health -= damage;
    }
}
