using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Color playerColor;
    public int playerColorIndex;
    public GameObject currentlyStandingFloor;
    public bool isAI;

    //private Color[] playerColorArray = {
    //    new Color(255, 169, 0, 255),
    //    new Color(248, 32, 64, 255),
    //    new Color(126, 0, 255, 255),
    //    new Color(0, 176, 41, 255)
    //};

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject != currentlyStandingFloor)
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    currentlyStandingFloor = hit.transform.gameObject;

                    if (isAI)
                        gameObject.GetComponent<AIController>().SetCurrentlyStandingFloor();
                }
            }
        }
    }
}
