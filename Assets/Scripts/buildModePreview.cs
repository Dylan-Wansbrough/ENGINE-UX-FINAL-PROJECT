using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildModePreview : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    bool buildingPreview;
    int buildNum;

    public Mesh[] trap;

    public Material invalidMat;
    public Material validMat;

    public GameObject placing;
    public GameObject area;
    public Vector2[] hitArea;
    MeshFilter m_mesh;
    MeshRenderer m_Texture;

    public LayerMask mask1;


    void Start()
    {
        m_mesh = placing.GetComponent<MeshFilter>();
        m_Texture = placing.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        buildingPreview = player.GetComponent<playerController>().buildMode;
        buildNum = player.GetComponent<playerController>().trapButton;

        if (buildingPreview)
        {

                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, mask1))
                {
                    if(hit.transform.gameObject.tag != "Ignore") { 
                        placing.transform.position = hit.point;
                        area.transform.localScale = new Vector3(hitArea[buildNum-1].x, 1, hitArea[buildNum-1].y);
                    }
                    
                    m_mesh.mesh = trap[buildNum - 1];
                    if (hit.transform.gameObject.tag != "Trap" && hit.transform.gameObject.tag != "Player" && hit.transform.gameObject.tag != "Enemy" && hit.transform.gameObject.tag != "AntiPlace")
                    {
                        m_Texture.material = validMat;
                    }
                    else{
                        m_Texture.material = invalidMat;
                    }

                }else{
                    placing.transform.position = new Vector3(300, 300, 300);
                }

        }
        else
        {
            placing.transform.position = new Vector3(300, 300, 300);
        }
    }
}
