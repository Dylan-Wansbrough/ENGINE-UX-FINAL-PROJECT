﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freezetrap : trapInheritance
{
    public GameObject visual;
    public float freeze;

    void Update()
    {
        if (POIController.gameOver != true)
        {
            if (target != null)
            {
                if (timer <= 0)
                {
                    Instantiate(visual, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
                    target.GetComponent<enemyInheritance>().freezeTime += freeze;
                    timer = timeBetweenSetOff;
                }
            }
            timer -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            target = other.gameObject;
        }
    }

    // Update is called once per frame
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            target = null;
        }
    }
}
