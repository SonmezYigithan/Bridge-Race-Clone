using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeConstructorScript : MonoBehaviour
{
    [SerializeField] Material[] brickMaterials;

    private StackManager stackManager;
    private GameObject player;
    private PlayerScript playerScript;

    private void Start()
    {
        stackManager = gameObject.transform.parent.GetComponent<StackManager>();
        player = gameObject.transform.parent.gameObject;
        playerScript = player.GetComponent<PlayerScript>();
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);

            if (hit.transform.gameObject.tag == "BridgeTile")
            {
                if (hit.transform.gameObject.GetComponent<MeshRenderer>().enabled == false)
                {
                    HandlePopping(hit);
                }
                else
                {
                    if (hit.transform.gameObject.GetComponent<BridgeTileScript>().colorIndex != playerScript.playerColorIndex)
                    {
                        if (stackManager.isPopable())
                        {
                            hit.transform.gameObject.GetComponent<BridgeTileScript>().ColorBrick(playerScript.playerColorIndex);
                            stackManager.Pop();
                        }
                        else
                        {

                        }
                    }
                }
            }

        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
        }
    }

    void HandlePopping(RaycastHit hit)
    {
        if (stackManager.isPopable())
        {
            hit.transform.gameObject.GetComponent<BridgeTileScript>().ColorBrick(playerScript.playerColorIndex);
            stackManager.Pop();
        }
        else
        {
            if (playerScript.isAI)
            {
                Debug.Log("Not Popable " + player.name);
                player.GetComponent<AIController>().ClearTarget();
                StartCoroutine(player.GetComponent<AIController>().GetTargets());
            }
            else
            {
                Debug.Log("enabled");
                hit.transform.GetChild(0).GetComponentInChildren<BoxCollider>().enabled = true;
            }
        }
    }
}
