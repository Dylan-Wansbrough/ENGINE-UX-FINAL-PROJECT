using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyInheritance : MonoBehaviour
{

    public float health;
    public int currencyWorth;
    public float playerRadius;
    public bool inRange;
    public GameObject Player;
    public GameObject Target;
    public GameObject focus;
    public NavMeshAgent agent;

    //Attacking
    public float attackRange;
    public float damageAmount;
    public float timeBetweenAttacks;
    public float timeTillNextAttack;


    public Animator EnemyAnims;
    public bool Dead;
    public float DeathTimeAmount;
    public float DeathTimer;


    public virtual void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Target = GameObject.FindGameObjectWithTag("POI");
        agent = GetComponent<NavMeshAgent>();
        Vector3 faceDir = new Vector3(10.65f, 7.64f, transform.position.z);
        transform.LookAt(faceDir);
    }

    public virtual void Update()
    {
        if(POIController.gameOver != true)
        {
            float tardist = Vector3.Distance(Target.transform.position, transform.position);
            float dist = Vector3.Distance(Player.transform.position, transform.position);
            if (tardist < playerRadius)
            {
                //overides chasing the player to go for the objective
                if (tardist < 1)
                {
                    POIController.PortalHealth--;
                    Destroy(gameObject);
                }

                inRange = false;
                focus = Target;
                agent.destination = Target.transform.position;

            }
            else if (dist < playerRadius)
            {
                inRange = true;
                focus = Player;
                agent.destination = Player.transform.position;

            }
            else
            {
                focus = Target;
                inRange = false;
                agent.destination = Target.transform.position;
            }

            targetDirection();

            if (health <= 0)
            {
                EnemyAnims.SetInteger("walkDir", 7);
                Dead = true;
                agent.destination = transform.position;
                if(DeathTimer >= DeathTimeAmount)
                {
                    playerController.trapCurrency += currencyWorth;
                    Destroy(gameObject);
                }
                DeathTimer += Time.deltaTime;
            }
        }
        else
        {
            agent.destination = gameObject.transform.position;
        }
    }

    public void targetDirection()
    {
        Vector3 dir = transform.position - focus.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (angle < 0f && angle >= -20f)
        {
            EnemyAnims.SetInteger("walkDir", 3);
        }
        else if (angle < -20f && angle >= -165f)
        {
            float checkX = transform.position.z - focus.transform.position.z;
            if (checkX >= 0)
            {
                EnemyAnims.SetInteger("walkDir", 4);
            }
            else
            {
                EnemyAnims.SetInteger("walkDir", 2);
            }
        }
        else
        {
            EnemyAnims.SetInteger("walkDir", 1);
        }
    }
}
