using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameUI : MonoBehaviour
{
    public GameObject player;
    public GameObject gameControl;

    public Text TrapCurrency;

    public Text POILives;
    public Text GameOver;
    public Text GameOver2;

    public Text round;
    public Text spawners;
    public Text enemies;

    public Text roundOver;
    public Text pressNext;

    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        TrapCurrency.text = "Trap Materials: " + playerController.trapCurrency;
        POILives.text = "20";
        GameOver.text = "";
        GameOver2.text = "";
        roundOver.text = "";
        pressNext.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        TrapCurrency.text = "Trap Materials: " + playerController.trapCurrency;
        POILives.text = POIController.PortalHealth.ToString();

        round.text = "Round: " + gameControl.GetComponent<gameController>().round;
        enemies.text = "Enemies: " + (gameControl.GetComponent<gameController>().totalSpawned - gameController.totalKilled);
        spawners.text = "Spawners: " + gameControl.GetComponent<gameController>().spawnerAmount;

        if (gameControl.GetComponent<gameController>().roundFinished)
        {
            roundOver.text = "Round " + gameControl.GetComponent<gameController>().round + " complete";
            pressNext.text = "Press F to start next round";
        }
        else
        {
            roundOver.text = "";
            pressNext.text = "";
        }

        if (gameController.isPaused)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }

        //game ends
        if (POIController.gameOver)
        {
            GameOver.text = "GAME OVER";
            GameOver2.text = "Press space to restart";
            if (Input.GetKeyDown("space"))
            {
                POIController.PortalHealth = 20;
                POIController.gameOver = false;
                playerController.trapCurrency = 100;
                gameController.totalKilled = 0;
                SceneManager.LoadScene("SampleScene");
            }
        }     
    }
}
