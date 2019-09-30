using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    public int direction;
    Vector2 velocity;
    public float speed;
    public Collider2D collider;

    public LayerMask combustable;
    public LayerMask enemy;
    public bool hitEnemy;

    public float lifeTime;
    float timer;
    float verticalRayDistance;
    RayOrigins rayOrigins;

    public int damage;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        DetectContact();
        DetectEnemyContact();


        timer += Time.deltaTime;
        velocity = Vector2.right * direction * speed;
        transform.Translate(velocity);


        if(timer > lifeTime)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void DetectEnemyContact()
    {
        if (direction == 1 && hitEnemy == false)
        {
            Vector2 colliderMax = new Vector2(collider.bounds.max.x, collider.bounds.max.y);
            Debug.DrawRay(colliderMax, Vector2.right);
            Vector2 colliderCenter = new Vector2(collider.bounds.max.x, collider.bounds.center.y);
            Debug.DrawRay(colliderCenter, Vector2.right);
            Vector2 colliderMin = new Vector2(collider.bounds.max.x, collider.bounds.min.y);
            Debug.DrawRay(colliderMin, Vector2.right);

            RaycastHit2D topRay = Physics2D.Raycast(colliderMax, Vector2.right, 1, enemy);
            RaycastHit2D midRay = Physics2D.Raycast(colliderCenter, Vector2.right, 1, enemy);
            RaycastHit2D botRay = Physics2D.Raycast(colliderMin, Vector2.right, 1, enemy);

            if (topRay || midRay || botRay)
            {
                print("hit detected");

                if (midRay)
                {
                    print(midRay.transform.name);
                    midRay.transform.gameObject.GetComponent<DamageDetector>().ApplyDamage(damage);
                    hitEnemy = true;
                    Destroy(gameObject);
                }
            }


        }
        else if(direction == -1 && hitEnemy == false)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            Vector2 colliderMin = new Vector2(collider.bounds.min.x, collider.bounds.min.y);
            Debug.DrawRay(collider.bounds.min, Vector2.left);
            Vector2 colliderCenter = new Vector2(collider.bounds.min.x, collider.bounds.center.y);
            Debug.DrawRay(colliderCenter, Vector2.left);
            Vector2 colliderMax = new Vector2(collider.bounds.min.x, collider.bounds.max.y);
            Debug.DrawRay(colliderMax, Vector2.left);

            RaycastHit2D topRay = Physics2D.Raycast(colliderMax, Vector2.right, 1, enemy);
            RaycastHit2D midRay = Physics2D.Raycast(colliderCenter, Vector2.right, 1, enemy);
            RaycastHit2D botRay = Physics2D.Raycast(colliderMin, Vector2.right, 1, enemy);

            if (topRay || midRay || botRay)
            {
                print("hit detected");

                if (midRay)
                {
                    print(midRay.transform.name);
                    midRay.transform.gameObject.GetComponent<DamageDetector>().ApplyDamage(damage);
                    hitEnemy = true;
                    Destroy(gameObject);
                }
            }



        }
    }

    public void DetectContact()
    {
        if (direction == 1)
        {
            Vector2 colliderMax = new Vector2(collider.bounds.max.x, collider.bounds.max.y);
            Debug.DrawRay(colliderMax, Vector2.right);
            Vector2 colliderCenter = new Vector2(collider.bounds.max.x, collider.bounds.center.y);
            Debug.DrawRay(colliderCenter, Vector2.right);
            Vector2 colliderMin = new Vector2(collider.bounds.max.x, collider.bounds.min.y);
            Debug.DrawRay(colliderMin, Vector2.right);

            RaycastHit2D topRay = Physics2D.Raycast(colliderMax, Vector2.right, 1, combustable);
            RaycastHit2D midRay = Physics2D.Raycast(colliderCenter, Vector2.right, 1, combustable);
            RaycastHit2D botRay = Physics2D.Raycast(colliderMin, Vector2.right, 1, combustable);

            if(topRay || midRay || botRay)
            {
                print("hit detected");

                if (midRay)
                {
                    print(midRay.transform.name);
                    midRay.transform.gameObject.GetComponent<FireStarter>().Ignite();
                }
            }


        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            Vector2 colliderMin = new Vector2(collider.bounds.min.x, collider.bounds.min.y);
            Debug.DrawRay(collider.bounds.min, Vector2.left);
            Vector2 colliderCenter = new Vector2(collider.bounds.min.x, collider.bounds.center.y);
            Debug.DrawRay(colliderCenter, Vector2.left);
            Vector2 colliderMax = new Vector2(collider.bounds.min.x, collider.bounds.max.y);
            Debug.DrawRay(colliderMax, Vector2.left);

            RaycastHit2D topRay = Physics2D.Raycast(colliderMax, Vector2.right, 1, combustable);
            RaycastHit2D midRay = Physics2D.Raycast(colliderCenter, Vector2.right, 1, combustable);
            RaycastHit2D botRay = Physics2D.Raycast(colliderMin, Vector2.right, 1, combustable);

            if (topRay || midRay || botRay)
            {
                print("hit detected");

                if (midRay)
                {
                    print(midRay.transform.name);
                    midRay.transform.gameObject.GetComponent<FireStarter>().Ignite();
                }
            }


            
        }

    }


    public struct RayOrigins
    {
        public Vector2 top, mid, bottom;
    }
}
