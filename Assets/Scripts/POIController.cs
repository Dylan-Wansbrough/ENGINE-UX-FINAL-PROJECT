using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIController : MonoBehaviour
{

    public static int PortalHealth = 20;
    public static bool gameOver = false;

    private void Update()
    {
        Debug.Log("Lives remaining " + PortalHealth);
        if (PortalHealth <= 0)
        {
            gameOver = true;
            Debug.Log("Game be Over");
        }
    }


}
