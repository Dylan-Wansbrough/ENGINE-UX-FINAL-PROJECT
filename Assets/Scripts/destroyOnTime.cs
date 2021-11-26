using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnTime : MonoBehaviour
{
    public float deathtime;
    float timer;
    

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > deathtime)
        {
            Destroy(gameObject);
        }
    }
}
