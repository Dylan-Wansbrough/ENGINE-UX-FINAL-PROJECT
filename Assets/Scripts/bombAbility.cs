using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombAbility : MonoBehaviour
{
    public Vector3 ExplosionLoc;
    Vector3 startingLoc;
    Vector3 dir;

    SphereCollider sphereCol;
    Rigidbody rig;

    bool exploding;

    public float explosionTimer;
    public float damage;
    public float explosionRadius;

    public GameObject explosion;
    public Queue<GameObject> waitingLine;

    private void Start()
    {
        sphereCol = gameObject.GetComponent<SphereCollider>();
        rig = gameObject.GetComponent<Rigidbody>();
        waitingLine = new Queue<GameObject>();

        startingLoc = gameObject.transform.position;

        // Calculate direction vector
        dir = ExplosionLoc - startingLoc;
        // Normalize resultant vector to unit Vector
        dir = dir.normalized;    
    }

    private void FixedUpdate()
    {
        if (!exploding)
        {
            gameObject.transform.position += dir * Time.deltaTime * 25;
        }
        if (exploding)
        {
            explosionTimer -= Time.deltaTime;
            if(explosionTimer <= 0)
            {
                Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
                int i = waitingLine.Count;
                while (i > 0)
                {
                    GameObject newGo = waitingLine.Dequeue();
                    newGo.GetComponent<enemyInheritance>().health -= damage;
                    i--;
                }
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!exploding)
        {
            if (collision.gameObject.layer == 10)
            {
                exploding = true;
                rig.isKinematic = true;
                sphereCol.isTrigger = true;
                sphereCol.radius = explosionRadius;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            waitingLine.Enqueue(other.gameObject);
        }
    }
}
