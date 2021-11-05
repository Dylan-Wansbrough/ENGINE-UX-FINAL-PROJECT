using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    public GameObject[] enemies;
    public int[] spawnChance;

    public float timeBetweenSpawns;
    float timer;


    // Update is called once per frame
    void Update()
    {
        if(timer <= 0)
        {
            int spawn = Random.Range(0, 100);

            int i = 0;
            while(i < spawnChance.Length)
            {
                if (spawn <= spawnChance[i])
                {
                    Instantiate(enemies[i], transform.position, Quaternion.identity);
                }
                i++;
            }
            
            timer = timeBetweenSpawns;
        }

        timer -= Time.deltaTime;
    }
}
