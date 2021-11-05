using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrap : trapInheritance
{
    public GameObject visual;

    // Start is called before the first frame update
    void Update()
    {
        if(target != null)
        {
            if(timer <= 0)
            {
                Instantiate(visual, transform.position, Quaternion.identity);
                target.GetComponent<enemyInheritance>().health -= damage;
                timer = timeBetweenSetOff;
            }
        }
        timer -= Time.deltaTime;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            target = other.gameObject;
        }
    }
}
