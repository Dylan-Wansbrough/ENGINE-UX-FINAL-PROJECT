using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerController : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject clickLocation;
    public GameObject clickAttack;
    public GameObject followCamera;
    public Vector3 repsawnLocation;

    //Attacking a target
    public GameObject target;
    public float targetradius;
    public float timeBetweenAttacks;
    float timeTillNextAttack;


    //player stats
    public float health;
    public float BasicAttackDam;

    //traps
    public GameObject[] traps;
    public int[] trapCost;
    public bool buildMode;
    public int trapButton;
    public static int trapCurrency = 100;


    //player movement and sprites
    public string direction;
    public GameObject spriteObject;
    SpriteRenderer spriRen;
    public Animator playerAnims;
    Vector3 lastMovement;


    public LayerMask mask1;
    public LayerMask mask2;
    public LayerMask mask3;


    //abilities
    public float[] cooldownDur;
    public float[] cooldowns;
    public float[] abilityDur;
    public bool iframe;

    bool pressed;
    void Start()
    {
        spriRen = spriteObject.GetComponent<SpriteRenderer>();
        spriRen.size += new Vector2(1f, 0.01f);
        agent = GetComponent<NavMeshAgent>();
        transform.LookAt(followCamera.transform);
    }

    void Update()
    {
        if (POIController.gameOver != true)
        {
            playerMovement();
            walkingAnims();

            if (target != null)
            {
                AutoAttacking();
            }


            if (health <= 0)
            {
                Debug.Log("Dead");
            }


            TrapMode();
            abilities();
            if(health > 100)
            {
                health = 100;
            }
            if (health <= 0)
            {
                gameObject.transform.position = repsawnLocation;
                agent.destination = transform.position;
                target = null;
                health = 100;
                trapCurrency -= 50;
                if (trapCurrency < 0) { trapCurrency = 0; }
            }
        }
        else
        {
            agent.destination = gameObject.transform.position;
        }
    }

    void playerMovement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            pressed = true;
        }
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, mask2))
            {
                agent.destination = hit.point;
                buildMode = false;
                target = null;
                playerDirection();
                if (pressed)
                {
                    Instantiate(clickLocation, hit.point, Quaternion.identity);
                    pressed = false;
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, mask1))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    float dist = Vector3.Distance(hit.transform.position, transform.position);
                    if (dist > targetradius)
                    {
                        target = hit.transform.gameObject;
                        agent.destination = hit.point;
                        playerDirection();
                    }
                    else
                    {
                        target = hit.transform.gameObject;
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
                targetDirection();
                if (target == null)
                {
                    agent.destination = transform.position;
                }
            }
        }
        timeTillNextAttack -= Time.deltaTime;
    }

    void TrapMode()
    {
        //turning on build mode
        if (Input.GetKeyDown("1"))
        {
            trapButton = 1;
            if (trapCurrency >= trapCost[trapButton - 1])
            {
                buildMode = true;
            }else
            {
                buildMode = false;
            }
        }
        else if (Input.GetKeyDown("2"))
        {
            trapButton = 2;
            if (trapCurrency >= trapCost[trapButton - 1])
            {
                buildMode = true;
            }
            else
            {
                buildMode = false;
            }
        }else if (Input.GetKeyDown("3"))
        {
            trapButton = 3;
            if (trapCurrency >= trapCost[trapButton - 1])
            {
                buildMode = true;
            }
            else
            {
                buildMode = false;
            }
        }

        if (buildMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, mask3))
                {
                    if (hit.transform.gameObject.tag != "Trap" && hit.transform.gameObject.tag != "Player" && hit.transform.gameObject.tag != "Enemy" && hit.transform.gameObject.tag != "AntiPlace")
                    {
                        if(trapCurrency >= trapCost[trapButton - 1])
                        {
                            trapCurrency -= trapCost[trapButton - 1];
                            buildMode = false;
                            Instantiate(traps[trapButton - 1], hit.point, Quaternion.identity);
                        }
                        
                    }

                }
            }
        }
    }

    void playerDirection()
    {
        Vector3 dir = transform.position - agent.destination;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        

        if (angle > 0f && angle < 20f)
        {
            direction = "up";
        }
        else if (angle > 20f && angle <= 165f)
        {
            float checkX = transform.position.z - agent.destination.z;
            if (checkX >= 0)
            {
                direction = "right";
            }
            else
            {
                direction = "left";
            }
        }
        else
        {
            direction = "down";
        }
    }

    void targetDirection()
    {
        Vector3 dir = transform.position - target.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;


        if (angle > 0f && angle < 20f)
        {
            direction = "up";
            playerAnims.SetInteger("walkDir", 13);
        }
        else if (angle > 20f && angle <= 165f)
        {
            float checkX = transform.position.z - target.transform.position.z;
            if (checkX >= 0)
            {
                direction = "right";
                playerAnims.SetInteger("walkDir", 14);
            }
            else
            {
                direction = "left";
                playerAnims.SetInteger("walkDir", 12);
            }
        }
        else
        {
            direction = "down";
            playerAnims.SetInteger("walkDir", 11);
        }
    }

    void walkingAnims()
    {
        bool notMoving = false;
        if(lastMovement == transform.position)
        {
            notMoving = true;
        }
        switch (direction)
        {
            case "up":
                if (notMoving){
                    playerAnims.SetInteger("walkDir", 5);
                }
                else{
                    playerAnims.SetInteger("walkDir", 3);
                }
                break;
            case "left":
                if (notMoving)
                {
                    playerAnims.SetInteger("walkDir", 5);
                }
                else
                {
                    playerAnims.SetInteger("walkDir", 2);
                }
                break;
            case "right":
                if (notMoving)
                {
                    playerAnims.SetInteger("walkDir", 5);
                }
                else
                {
                    playerAnims.SetInteger("walkDir", 4);
                }
                break;
            case "down":
                if (notMoving)
                {
                    playerAnims.SetInteger("walkDir", 5);
                }
                else
                {
                    playerAnims.SetInteger("walkDir", 1);
                }
                break;
            default:
                Debug.Log("Invalid direction");
                break;
        }

        lastMovement = transform.position;
    }

    void abilities()
    {
        //Q ability 
        if (Input.GetKeyDown("q"))
        {
            if (cooldowns[0] <= 0)
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, mask2))
                {
                    agent.speed = 100;
                    agent.acceleration = 100;
                    agent.destination = hit.point;
                    buildMode = false;
                    target = null;
                    iframe = true;
                    playerDirection();
                    Instantiate(clickLocation, hit.point, Quaternion.identity);
                    cooldowns[0] = cooldownDur[0];
                }
            }
        }

        if(cooldowns[0] <= (cooldownDur[0] - abilityDur[0]))
        {
            agent.speed = 10;
            iframe = false;
            agent.acceleration = 40;
        }

        if(cooldowns[0] > 0)
        {
            cooldowns[0] -= Time.deltaTime;
        }
        else { cooldowns[0] = 0; }


        //W ability 

        //E ability
        if (Input.GetKeyDown("e"))
        {
            if (cooldowns[2] <= 0)
            {
                timeBetweenAttacks = 0.2f;
                cooldowns[2] = cooldownDur[2];
            }
        }

        if (cooldowns[2] <= (cooldownDur[2] - abilityDur[2]))
        {
            timeBetweenAttacks = 0.4f;
        }

        if (cooldowns[2] > 0)
        {
            cooldowns[2] -= Time.deltaTime;
        }
        else { cooldowns[2] = 0; }
    }
}
