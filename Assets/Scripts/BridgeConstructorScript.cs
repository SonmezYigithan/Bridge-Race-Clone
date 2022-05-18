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
                    if (stackManager.isPopable())
                    {
                        hit.transform.gameObject.GetComponent<BridgeTileScript>().ColorBrick(playerScript.playerColorIndex);
                        stackManager.Pop();
                    }
                    else
                    {
                        if (playerScript.isAI)
                        {
                            // stop and go back for collect more
                            Debug.Log("Not Popable " + player.name);
                            // targets empty ise GetTargets yap
                            StartCoroutine(player.GetComponent<AIController>().GetTargets());
                        }
                    }
                }
                else
                {
                    if(hit.transform.gameObject.GetComponent<BridgeTileScript>().colorIndex != playerScript.playerColorIndex)
                    {
                        if (stackManager.isPopable())
                        {
                            hit.transform.gameObject.GetComponent<BridgeTileScript>().ColorBrick(playerScript.playerColorIndex);
                            stackManager.Pop();
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
}
