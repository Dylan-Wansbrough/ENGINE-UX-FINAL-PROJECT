using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public bool PlayerBool;

    public GameObject player;
    public GameObject Enemy;


    Image bar;

    float maxHealth;
    float currenthealth;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerBool)
        {
            maxHealth = 100;
        }
        else
        {
            maxHealth =  Enemy.GetComponent<enemyInheritance>().health;
        }

        bar = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        //get cooldown
        if (PlayerBool)
        {
            currenthealth = player.GetComponent<playerController>().health;
        }
        else
        {
            currenthealth = Enemy.GetComponent<enemyInheritance>().health;
        }
            


        //percentage of
        float fraction = maxHealth / currenthealth;
        float percentage = 100 / fraction;
        bar.fillAmount = (percentage / 100);
    }
}
