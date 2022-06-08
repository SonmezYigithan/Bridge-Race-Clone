using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Color playerColor;
    public int playerColorIndex;
    public GameObject currentlyStandingFloor;
    public bool isAI;

    private void Update()
    {
        FindCurrentlyStandingFloor();
    }

    void FindCurrentlyStandingFloor()
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
