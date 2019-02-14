using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI Player1, Player2, Player3, Player4;
    public TextMeshProUGUI playerScores;
    public TextMeshProUGUI winner;
    public GameObject panel;


    // Start is called before the first frame update
    void Awake()
    {
        GameManager.GM.UIManager = this;
    }

    public void SetScores(int[] players)
    {
        string scores = "";
        for (int i = 0; i<players.Length; i++)
        {
            scores += "| Player " + (i+1) + " = " + players[i] + " |";
        }
        playerScores.text = scores;
    }

    public void SetWinner(int playerNum)
    {
        panel.SetActive(true);
        winner.text = "Player " + playerNum + " Won!";
    }


}
