using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VistenalliAI : MonoBehaviour
{
    [SerializeField]
    private GameObject bubble;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private PlayerDirection myDirection;
    [SerializeField]
    private EnemyHealth enemyHealth;

    public enum States
    {
        Normal,
        Enraged,
        Ballistic
    }
    [SerializeField]
    private int stateNumber;

    private float timer;
    [SerializeField]
    private float attackSpeed;
    // Start is called before the first frame update
    void Start()
    {
        stateNumber = (int)States.Normal;
        attackSpeed = 4;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchStates();
        timer += Time.deltaTime;

        if(stateNumber == (int)States.Normal)
        {
            NormalAttack();
        }else if(stateNumber == (int)States.Enraged)
        {
            EnragedAttack();
        }else if(stateNumber == (int)States.Ballistic)
        {
            BallisticAttack();
        }
    }

    void SpawnBubble()
    {
        GameObject attackBubble = Instantiate(bubble);
        attackBubble.transform.position = spawnPoint.position;
        attackBubble.GetComponent<ProjectileHandler>().direction = myDirection.direction;
    }

    void SwitchStates()
    {
        if(enemyHealth.health < 30 && enemyHealth.health > 10)
        {
            stateNumber = (int)States.Enraged;
            attackSpeed = 2;
        }else if(enemyHealth.health < 10)
        {
            stateNumber = (int)States.Ballistic;
            attackSpeed = 1;
        }
    }

    void NormalAttack()
    {
        if (timer > attackSpeed)
        {
            SpawnBubble();
            timer = 0;
        }
    }

    void EnragedAttack()
    {
        if (timer > attackSpeed)
        {
            SpawnBubble();
            timer = 0;
        }
    }

    void BallisticAttack()
    {
        if (timer > attackSpeed)
        {
            SpawnBubble();
            timer = 0;
        }
    }
}
