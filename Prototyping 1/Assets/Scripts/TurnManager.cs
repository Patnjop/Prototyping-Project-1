using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private int roundNumber = 1;
    private int playerAmount;
    private List<PlayerInput> playerInputs = new List<PlayerInput>();
    

    private void Start()
    {
        GameManager.GM.StartGame(this);
        playerAmount = GameManager.GM.PlayerAmount();
        for (int i = 0; i < playerAmount; i++)
        {
            playerInputs.Add(gameObject.AddComponent<PlayerInput>());
            playerInputs[i].turnManager = this;
        }
    }

    public void startRound()
    {
        foreach (PlayerInput p in playerInputs)
        {
            p.GetInput();
        }
    }

    
}
