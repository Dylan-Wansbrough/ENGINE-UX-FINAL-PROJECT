using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    public GameObject clickLocation;
    public GameObject clickAttack;
    public GameObject followCamera;

    //Attacking a target
    GameObject target;
    public float targetradius;
    public float timeBetweenAttacks;
    float timeTillNextAttack;


    //player stats
    public float health;
    public float BasicAttackDam;

    //traps
    public GameObject[] traps;
    public bool buildMode;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        playerMovement();

        if(target != null)
        {
            AutoAttacking();
        }
        

        if(health <= 0)
        {
            Debug.Log("Dead");
        }


        TrapMode();
    }

    void playerMovement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
                buildMode = false;
                target = null;
                Instantiate(clickLocation, hit.point, Quaternion.identity);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    float dist = Vector3.Distance(hit.transform.position, transform.position);
                    if (dist > targetradius)
                    {
                        target = hit.transform.gameObject;
                        agent.destination = hit.point;
                        
                    }
                    else
                    {
                        target = hit.transform.gameObject;
                        agent.isStopped = true;
                        Quaternion newRotation = Quaternion.LookRotation(hit.point - transform.position);
                        newRotation.x = 0f;
                        newRotation.z = 0f;
                        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 10);
                        agent.isStopped = false;
                    }
                    buildMode = false;
                    Instantiate(clickAttack, hit.point, Quaternion.identity);
                }

            }
        }

        //if the player has a current target
        if(target != null)
        {
            float dist = Vector3.Distance(target.transform.position, transform.position);
            if(dist < targetradius)
            {
                agent.destination = transform.position;
            }
        }

        followCamera.transform.position = new Vector3(gameObject.transform.position.x + 12, gameObject.transform.position.y + 10, gameObject.transform.position.z);
    }

    void AutoAttacking()
    {
        float dist = Vector3.Distance(target.transform.position, transform.position);
        if (dist < targetradius)
        {
            if(timeTillNextAttack <= 0)
            {
                timeTillNextAttack = timeBetweenAttacks;
                target.GetComponent<enemyInheritance>().health -= BasicAttackDam;
                if(target == null)
                {
                    agent.destination = transform.position;
                }
                Debug.Log("Attacking for " + BasicAttackDam + "damage.");
            }
        }
        timeTillNextAttack -= Time.deltaTime;
    }

    void TrapMode()
    {
        //turning on build mode
        if (Input.GetKeyDown("1"))
        {
            buildMode = true;
        }

        if (buildMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    if (hit.transform.gameObject.tag != "Enemy")
                    {
                        Instantiate(traps[0], hit.point, Quaternion.identity);
                    }

                }
            }
        }
    }
}
