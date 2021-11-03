using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyInheritance : MonoBehaviour
{

    public float health;
    public float playerRadius;
    public bool inRange;
    public GameObject Player;
    public GameObject Target;
    public NavMeshAgent agent;

    //Attacking
    public float attackRange;
    public float damageAmount;
    public float timeBetweenAttacks;
    public float timeTillNextAttack;


    public virtual void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Target = GameObject.FindGameObjectWithTag("POI");
        agent = GetComponent<NavMeshAgent>();
    }

    public virtual void Update()
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
            agent.destination = Target.transform.position;

        }else if (dist < playerRadius)
        {
            inRange = true;
            agent.destination = Player.transform.position;

        }else
        {
            inRange = false;
            agent.destination = Target.transform.position;
        }
        



        if (health <= 0)
        {
            Destroy(gameObject);
        }

        
    }
}
