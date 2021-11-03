using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIController : MonoBehaviour
{

    public static int PortalHealth = 20;


    private void Update()
    {
        Debug.Log("Lives remaining " + PortalHealth);
        if (PortalHealth < 0)
        {
            Debug.Log("Game be Over");
        }
    }


}
