using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    public GameObject clickLocation;
    public GameObject followCamera;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
                Instantiate(clickLocation, hit.point, Quaternion.identity);
            }
        }else if(Input.GetMouseButtonDown(0)){
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if(hit.transform.gameObject.tag == "Enemy")
                {

                    Instantiate(clickLocation, hit.point, Quaternion.identity);
                }
                
            }
        }

        followCamera.transform.position = new Vector3(gameObject.transform.position.x + 12, gameObject.transform.position.y + 10, gameObject.transform.position.z);
    }
}
