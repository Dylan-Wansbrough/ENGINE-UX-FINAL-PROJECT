using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] Doors;
    public GameObject[] DoorIcon;
    public GameObject[] SpawnIcons;
    public GameObject player;

    public int round;

    public int totalSpawned;
    public static int totalKilled;

    bool roundStarted;


    private spawnvalues spawnvalues;
    public spawnvalues[] waveVals;

    public int spawnerAmount;

    public bool roundFinished;

    float boostHealthAmount;

    public int enemeyType = 1;

    public static bool isPaused;

    //audio
    public AudioSource audi;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        round = 1;
        boostHealthAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(POIController.gameOver != true)
        {
            if (Input.GetKeyDown("escape"))
            {
                if (isPaused)
                {
                    isPaused = false;
                    Time.timeScale = 1;
                }
                else
                {
                    isPaused = true;
                    Time.timeScale = 0;
                }
                
            }

            if (!roundStarted)
            {
                player.GetComponent<playerController>().health += 10;

                //music related
                int enemyNum = Random.Range(1, 3); //how many different enemy types
                if(enemyNum != enemeyType)
                {
                    enemeyType = enemyNum;
                    audi.clip = clips[enemeyType - 1];
                    audi.Play();
                    if(enemeyType == 1)
                    {
                        audi.volume = 0.1f;
                    }
                    else
                    {
                        audi.volume = 0.2f;
                    }
                }

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
                else if (round < 21)
                {
                    spawnvalues = waveVals[3];
                }
                else
                {
                    spawnvalues = waveVals[4];
                }

                if (round != 1)
                {
                    boostHealthAmount += 0.025f;
                    if (boostHealthAmount > 3) { boostHealthAmount = 3; }
                    player.GetComponent<playerController>().BasicAttackDam = 25 * boostHealthAmount;
                }

                int p = 0;
                while (p < Doors.Length)
                {
                    Doors[p].SetActive(false);
                    DoorIcon[p].SetActive(false);
                    p++;
                }

                int b = 0;
                while (b < SpawnIcons.Length)
                {
                    SpawnIcons[b].SetActive(false);
                    b++;
                }

                int doorNum = Random.Range(0, Doors.Length + 5);
                if (doorNum < Doors.Length) { Doors[doorNum].SetActive(true); DoorIcon[doorNum].SetActive(true); }

                spawnerAmount = Random.Range(spawnvalues.minSpawner, spawnvalues.maxSpawner + 1);

                List<int> previousNum = new List<int>();
                int i = 0;
                while (i < spawnerAmount)
                {
                    int spawnerNum = Random.Range(0, spawnPoints.Length);
                    if (i != 0)
                    {
                        int r = 0;
                        while (r < previousNum.Count)
                        {
                            if (spawnerNum != previousNum[r])
                            {
                                r++;
                            }
                            else
                            {
                                r = 0;
                                spawnerNum = Random.Range(0, spawnPoints.Length);
                            }
                        }
                    }
                    previousNum.Add(spawnerNum);
                    //spawn more zombies
                    int spawnAmount = 0;
                    if (enemeyType == 2)
                    {
                        spawnAmount = Random.Range(spawnvalues.minAmount, spawnvalues.maxAmount + 10);
                    }
                    else
                    {
                        spawnAmount = Random.Range(spawnvalues.minAmount, spawnvalues.maxAmount + 1);
                    }
                    
                    if ((spawnAmount % 2) != 0)
                    {
                        spawnAmount++;
                    }
                    totalSpawned += spawnAmount;
                    spawnPoints[spawnerNum].GetComponent<enemySpawner>().spawnAmount = spawnAmount;
                    int spawnTimer = 0;
                    //make rounds harder if only one spawner
                    if (spawnerAmount == 1)
                    {
                        spawnTimer = Random.Range(spawnvalues.minTime, spawnvalues.maxTime + 1);
                        spawnTimer = spawnTimer / 2;
                    }
                    else
                    {
                        spawnTimer = Random.Range(spawnvalues.minTime, spawnvalues.maxTime + 1);
                    }
                    spawnPoints[spawnerNum].GetComponent<enemySpawner>().enemyNumType = enemeyType;

                    spawnPoints[spawnerNum].GetComponent<enemySpawner>().timeBetweenSpawns = spawnTimer;

                    //boost enemy health
                    spawnPoints[spawnerNum].GetComponent<enemySpawner>().spawnHealthBoost = boostHealthAmount;

                    if(enemeyType == 2)
                    {
                        spawnPoints[spawnerNum].GetComponent<enemySpawner>().spawnAtOnce = Random.Range(2, spawnvalues.spawnAmount + 1);
                    }
                    else
                    {
                        spawnPoints[spawnerNum].GetComponent<enemySpawner>().spawnAtOnce = Random.Range(1, spawnvalues.spawnAmount + 1);
                    }
                    


                    i++;
                }
                previousNum.Clear();
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
}
