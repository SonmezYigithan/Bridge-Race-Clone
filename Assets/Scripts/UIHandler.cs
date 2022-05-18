using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public GameObject tapToStartPanel;
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    public void TapToStart()
    {
        tapToStartPanel.SetActive(false);
        GameManager.instance.StartTheGame();
    }
}
