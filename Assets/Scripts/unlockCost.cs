using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unlockCost : MonoBehaviour
{
    public int trapNum;
    public float TrapCost;
    
    public GameObject player;
    Image faded;

    // Start is called before the first frame update
    void Start()
    {
        TrapCost = player.GetComponent<playerController>().trapCost[trapNum];
        faded = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.trapCurrency < TrapCost)
        {
            //percentage of
            float fraction = TrapCost / playerController.trapCurrency;
            float percentage = 100 / fraction;
            float actual = 100 - percentage;
            faded.fillAmount = (actual / 100);
        }
        else
        {
            faded.fillAmount = 0;
        }

        
    }
}
