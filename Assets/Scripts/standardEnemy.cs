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

        if (inRange)
        {
            float dist = Vector3.Distance(Player.transform.position, transform.position);
            if (dist < attackRange)
            {
                if (timeTillNextAttack <= 0)
                {
                    timeTillNextAttack = timeBetweenAttacks;
                    Player.GetComponent<playerController>().health -= damageAmount;
                    //Add knockback in later please
                    Debug.Log("Attacking Player for " + damageAmount + " damage.");
                }
            }
        }

        timeTillNextAttack -= Time.deltaTime;
    }
}
