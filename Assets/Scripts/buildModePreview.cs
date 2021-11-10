using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildModePreview : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    bool buildingPreview;
    int buildNum;

    public Mesh[] cantplace;
    public Mesh[] okayplace;

    public GameObject placing;


    void Start()
    {
        //placing = Instantiate(cantplace[0], new Vector3(3000, 300, 3000), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        buildingPreview = player.GetComponent<playerController>().buildMode;
        buildNum = player.GetComponent<playerController>().trapButton;

        if (buildingPreview)
        {

                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    
                    if (hit.transform.gameObject.tag != "Trap" && hit.transform.gameObject.tag != "Player" && hit.transform.gameObject.tag != "Enemy" && hit.transform.gameObject.tag != "AntiPlace")
                    {
                        //placing = okayplace[buildNum-1];
                    }else{
                        //placing = cantplace[buildNum-1];
                    }

                }else{
                    placing = null;
                }

        }
    }
}
