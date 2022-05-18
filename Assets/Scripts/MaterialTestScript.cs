using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTestScript : MonoBehaviour
{
    Renderer renderer;

    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        renderer.material.color = Color.green;
        renderer.material.SetColor("_Color", Color.green);
    }

    
}
