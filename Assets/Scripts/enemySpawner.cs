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

    public GameObject mapIcon;

    // Update is called once per frame
    void Update()
    {
        if (POIController.gameOver != true)
        {
            if (timer <= 0 && spawnAmount > 0)
            {
                mapIcon.SetActive(true);
                int spawn = Random.Range(0, 100);
                spawnAmount--;
                if (spawn <= spawnChance[0])
                {
                    GameObject newGo = Instantiate(enemies[0], transform.position, Quaternion.identity);
                    newGo.GetComponent<enemyInheritance>().health = newGo.GetComponent<enemyInheritance>().health * spawnHealthBoost;
                }else if (spawn <= spawnChance[1])
                {
                    GameObject newGo = Instantiate(enemies[1], transform.position, Quaternion.identity);
                    newGo.GetComponent<enemyInheritance>().health = newGo.GetComponent<enemyInheritance>().health * spawnHealthBoost;
                }

                timer = timeBetweenSpawns;
            }

            timer -= Time.deltaTime;
        }
    }
}
