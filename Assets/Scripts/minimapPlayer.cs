using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapPlayer : MonoBehaviour
{

    public GameObject player;
    RectTransform m_RectTransform;

    private void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            Destroy(gameObject);
        }
        else
        {
            float yPos = Remap(player.transform.position.x, -138.07f, -236.38f, -506.5f, -342.57f);
            float xPos = Remap(player.transform.position.z, -81.07f, 98.29f, -872.45f, -562.4f); //fix this
            m_RectTransform.anchoredPosition = new Vector2(xPos, yPos);
        }
    }

    float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        float fromAbs = from - fromMin;
        float fromMaxAbs = fromMax - fromMin;

        float normal = fromAbs / fromMaxAbs;

        float toMaxAbs = toMax - toMin;
        float toAbs = toMaxAbs * normal;

        float to = toAbs + toMin;

        return to;
    }
}
