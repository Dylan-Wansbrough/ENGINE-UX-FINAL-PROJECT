using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrap : trapInheritance
{
    public GameObject visual;
    public Animator anim;

    public bool visualBool;

    // Start is called before the first frame update
    void Update()
    {
        if (POIController.gameOver != true)
        {
            if (target != null)
            {
                if (timer <= 0)
                {
                    if (visualBool)
                    {
                        Instantiate(visual, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
                    }
                    else
                    {
                        audi.Play();
                        anim.SetTrigger("Stepped on");
                    }

                    target.GetComponent<enemyInheritance>().health -= damage;
                    timer = timeBetweenSetOff;
                }
            }
            timer -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
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
