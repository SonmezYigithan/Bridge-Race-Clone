using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCompleteHandler : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && GetComponent<MeshRenderer>().enabled)
        {
            // spawn new items
        }
    }
}
