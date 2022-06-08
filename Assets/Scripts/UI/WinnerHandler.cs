using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinnerHandler : MonoBehaviour
{
    public TMP_Text winnerText;
    public GameObject resultPanel;

    private void OnTriggerEnter(Collider other)
    {
        AnnounceWinner(other);
    }

    void AnnounceWinner(Collider other)
    {
        Debug.Log("Winner is " + other.gameObject.name);
        gameObject.GetComponent<Collider>().enabled = false;
        GameManager.instance.ShowWinPanel(other.gameObject.name);
    }
}
