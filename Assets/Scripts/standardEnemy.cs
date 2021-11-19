using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class standardEnemy : enemyInheritance
{
    

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (inRange && !Dead)
        {
            float dist = Vector3.Distance(Player.transform.position, transform.position);
            if (dist < attackRange)
            {
                if (timeTillNextAttack <= 0)
                {
                    EnemyAnims.SetInteger("walkDir", 6);
                    timeTillNextAttack = timeBetweenAttacks;
                    if(Player.GetComponent<playerController>().iframe == false)
                    {
                        Player.GetComponent<playerController>().health -= damageAmount;
                        Instantiate(playerBlood, Player.transform.position, Quaternion.identity);
                        Debug.Log("Attacking Player for " + damageAmount + " damage.");
                    }
                }
            }
        }

        timeTillNextAttack -= Time.deltaTime;
    }
}
