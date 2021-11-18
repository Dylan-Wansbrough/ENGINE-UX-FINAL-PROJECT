using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POIController : MonoBehaviour
{

    public static int PortalHealth = 20;
    public static bool gameOver = false;
    public SpriteRenderer inner;

    float alpha = 0.2f;
    bool up;
    Color col = new Color(0, 0.9418654f, 1);

    private void Update()
    {
        if (PortalHealth <= 0)
        {
            gameOver = true;
            Debug.Log("Game be Over");
        }

        if(up){
          alpha += 0.001f;
          if(alpha > 0.4f){
            up =  false;
          }
        }else{
          alpha -= 0.001f;
          if(alpha < 0f){
            up =  true;
          }
        }
        col.a = alpha;
        inner.color = col;
    }


}
