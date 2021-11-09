using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldownScript : MonoBehaviour
{
    public GameObject player;

    public int abilityNum;
    public float maxCooldown;
    public float currentCooldown;

    Image faded;

    // Start is called before the first frame update
    void Start()
    {
        maxCooldown = player.GetComponent<playerController>().cooldownDur[abilityNum];
        faded = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //get cooldown
        currentCooldown = player.GetComponent<playerController>().cooldowns[abilityNum];


        //percentage of
        float fraction = maxCooldown / currentCooldown;
        float percentage = 100 /fraction;
        faded.fillAmount = (percentage / 100);
    }
}
