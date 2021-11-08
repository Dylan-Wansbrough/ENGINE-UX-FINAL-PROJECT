using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameUI : MonoBehaviour
{
    public GameObject player;

    public Text Health;
    public Text TrapCurrency;

    public Text POILives;
    public Text GameOver;

    // Start is called before the first frame update
    void Start()
    {
        Health.text = "Health: " + player.GetComponent<playerController>().health;
        TrapCurrency.text = "Trap Materials: " + playerController.trapCurrency;
        POILives.text = "20";
        GameOver.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        Health.text = "Health: " + player.GetComponent<playerController>().health;
        TrapCurrency.text = "Trap Materials: " + playerController.trapCurrency;
        POILives.text = POIController.PortalHealth.ToString();

        //game ends
        if (POIController.gameOver)
        {
            GameOver.text = "GAME OVER";
        }     
    }
}
