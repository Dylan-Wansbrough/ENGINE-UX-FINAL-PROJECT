using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public GameObject[] spawnPoints;

    public int round;

    public int totalSpawned;
    public static int totalKilled;

    bool roundStarted;


    private spawnvalues spawnvalues;
    public spawnvalues[] waveVals;

    public int spawnerAmount;

    public bool roundFinished;

    // Start is called before the first frame update
    void Start()
    {
        round = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!roundStarted)
        {
            if (round < 4)
            {
                spawnvalues = waveVals[0];
            }
            else if (round < 8)
            {
                spawnvalues = waveVals[1];
            }
            else if (round < 13)
            {
                spawnvalues = waveVals[2];
            }
            else
            {
                spawnvalues = waveVals[3];
            }


            spawnerAmount = Random.Range(spawnvalues.minSpawner, spawnvalues.maxSpawner + 1);

            int previousNum = 10;
            int i = 0;
            while (i < spawnerAmount)
            {
                int spawnerNum = Random.Range(0, spawnPoints.Length);
                while(spawnerNum == previousNum)
                {
                    spawnerNum = Random.Range(0, spawnPoints.Length);
                }
                previousNum = spawnerNum;
                int spawnAmount = Random.Range(spawnvalues.minAmount, spawnvalues.maxAmount + 1);
                totalSpawned += spawnAmount;
                spawnPoints[spawnerNum].GetComponent<enemySpawner>().spawnAmount = spawnAmount;
                int spawnTimer = Random.Range(spawnvalues.minTime, spawnvalues.maxTime + 1);
                spawnPoints[spawnerNum].GetComponent<enemySpawner>().timeBetweenSpawns = spawnTimer;
                i++;
            }
            roundStarted = true;
        }

        if (totalSpawned == totalKilled)
        {
            roundFinished = true;
            if (Input.GetKeyDown("f"))
            {
                //start next round
                round++;
                roundStarted = false;
                roundFinished = false;
                totalSpawned = 0;
                totalKilled = 0;
            }
        }
        
    }
}
