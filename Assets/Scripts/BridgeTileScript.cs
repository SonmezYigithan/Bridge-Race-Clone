using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTileScript : MonoBehaviour
{
    public int colorIndex;
    [SerializeField] Material[] brickMaterials;
    [SerializeField] private Renderer brickRenderer;
    
    float timeLeft;
    bool animateColor = false;
    int targetColorIndex;
    Color targetColor;

    public void ColorBrick(int _targetColorIndex)
    {
        brickRenderer.enabled = true;
        brickRenderer.material.SetColor("_Color", brickMaterials[_targetColorIndex].color);

        colorIndex = _targetColorIndex;

        //StartCoroutine(AnimateColor());

        targetColor = brickMaterials[_targetColorIndex].color;


        transform.GetChild(0).GetComponentInChildren<BoxCollider>().enabled = false;
    }

    //IEnumerator AnimateColor()
    //{
    //    brickRenderer.material.color = Color.white;
    //    while (Color.Equals(brickRenderer.material.color, targetColor))
    //    {
    //        brickRenderer.material.color = Color.Lerp(brickRenderer.material.color, targetColor, 1); 
    //        yield return new WaitForSeconds(0.5f);
    //    }

    //    yield return null;
    //}

}
