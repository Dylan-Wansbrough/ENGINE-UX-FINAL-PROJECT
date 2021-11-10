using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    public GameObject[] enemies;
    public int[] spawnChance;

    public float timeBetweenSpawns;
    float timer;

    public float spawnAmount;
    public float spawnHealthBoost;


    // Update is called once per frame
    void Update()
    {
        if (POIController.gameOver != true)
        {
            if (timer <= 0 && spawnAmount > 0)
            {
                int spawn = Random.Range(0, 100);
                spawnAmount--;
                int i = 0;
                while (i < spawnChance.Length)
                {
                    if (spawn <= spawnChance[i])
                    {
                        GameObject newGo = Instantiate(enemies[i], transform.position, Quaternion.identity);
                        newGo.GetComponent<enemyInheritance>().health = newGo.GetComponent<enemyInheritance>().health * spawnHealthBoost;
                    }
                    i++;
                }

                timer = timeBetweenSpawns;
            }

            timer -= Time.deltaTime;
        }
    }
}
