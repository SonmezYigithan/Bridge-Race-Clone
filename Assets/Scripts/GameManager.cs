using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject player;

    public static GameManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        Time.timeScale = 0;
    }

    void Update()
    {
        ShowLosePanelIfFall();
    }

    void ShowLosePanelIfFall()
    {
        if (player.transform.position.y < -15)
        {
            losePanel.SetActive(true);
        }
    }

    public void ShowWinPanel(string winnerName)
    {
        winPanel.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "Winner is " + winnerName;
        winPanel.SetActive(true);
    }

    public void StartTheGame()
    {
        Time.timeScale = 1;
    }
}
