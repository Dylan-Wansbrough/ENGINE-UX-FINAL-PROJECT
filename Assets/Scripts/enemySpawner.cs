using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    public GameObject[] enemies;
    public int[] spawnChance;

    public float timeBetweenSpawns;
    float timer;

    public float spawnAtOnce;

    public float spawnAmount;
    public float spawnHealthBoost;

    public GameObject mapIcon;

    public SpriteRenderer inner;

    float alpha = 0.2f;
    bool up;
    Color col = new Color(0.7735849f, 0.08392668f, 0.08392668f);

    public Light lighty;

    // Update is called once per frame
    void Update()
    {
        if (POIController.gameOver != true)
        {
            if (timer <= 0 && spawnAmount > 0)
            {
                mapIcon.SetActive(true);
                int i = 0;
                while (i < spawnAtOnce)
                {
                    int spawn = Random.Range(0, 100);
                    spawnAmount--;
                    if (spawn <= spawnChance[0])
                    {
                        GameObject newGo = Instantiate(enemies[0], transform.position, Quaternion.identity);
                        newGo.GetComponent<enemyInheritance>().health = newGo.GetComponent<enemyInheritance>().health * spawnHealthBoost;
                    }
                    else if (spawn <= spawnChance[1])
                    {
                        GameObject newGo = Instantiate(enemies[1], transform.position, Quaternion.identity);
                        newGo.GetComponent<enemyInheritance>().health = newGo.GetComponent<enemyInheritance>().health * spawnHealthBoost;
                    }
                    else if (spawn <= spawnChance[2])
                    {
                        GameObject newGo = Instantiate(enemies[2], transform.position, Quaternion.identity);
                        newGo.GetComponent<enemyInheritance>().health = newGo.GetComponent<enemyInheritance>().health * spawnHealthBoost;
                    }

                    timer = timeBetweenSpawns;
                    i++;
                }
                
            }

            timer -= Time.deltaTime;
        }

        if (up)
        {
            alpha += 0.001f;
            lighty.range += 0.03f;
            if (alpha > 0.4f)
            {
                up = false;
            }
        }
        else
        {
            alpha -= 0.001f;
            lighty.range -= 0.03f;
            if (alpha < 0f)
            {
                up = true;
            }
        }
        col.a = alpha;
        inner.color = col;
    }
}
