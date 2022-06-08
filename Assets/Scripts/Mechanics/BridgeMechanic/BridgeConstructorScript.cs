using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeConstructorScript : MonoBehaviour
{
    [SerializeField] Material[] brickMaterials;

    private StackManager stackManager;
    private GameObject player;
    private PlayerScript playerScript;
    [SerializeField] private int bridgeIndexBiggest = 0;

    private void Start()
    {
        stackManager = gameObject.transform.parent.GetComponent<StackManager>();
        player = gameObject.transform.parent.gameObject;
        playerScript = player.GetComponent<PlayerScript>();
    }

    private void Update()
    {
        HandleBridgeConstruction();
    }

    void HandleBridgeConstruction()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.tag == "BridgeTile")
            {
                // for handling going backwards in the middle of the bridge
                if (bridgeIndexBiggest == 0 || bridgeIndexBiggest < int.Parse(hit.transform.gameObject.name))
                {
                    bridgeIndexBiggest = int.Parse(hit.transform.gameObject.name);
                    hit.transform.GetChild(0).GetComponentInChildren<BoxCollider>().enabled = false;
                }
                else if(bridgeIndexBiggest > int.Parse(hit.transform.gameObject.name))
                {
                    // going backwards can pass
                    hit.transform.GetChild(0).GetComponentInChildren<BoxCollider>().enabled = false;
                    return;
                }

                if (stackManager.isPopable())
                {
                    if (hit.transform.gameObject.GetComponent<MeshRenderer>().enabled == false ||
                        hit.transform.gameObject.GetComponent<BridgeTileScript>().colorIndex != playerScript.playerColorIndex)
                    {
                        hit.transform.gameObject.GetComponent<BridgeTileScript>().ColorBrick(playerScript.playerColorIndex);
                        stackManager.Pop();
                    }
                }
                else
                {
                    if (playerScript.isAI)
                    {
                        player.GetComponent<AIController>().ClearTarget();
                        StartCoroutine(player.GetComponent<AIController>().GetTargets());
                    }
                    else
                    {
                        hit.transform.GetChild(0).GetComponentInChildren<BoxCollider>().enabled = true;
                    }
                }
            }
            else if (hit.transform.gameObject.tag == "Floor")
            {
                bridgeIndexBiggest = 0;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
        }
    }
}
