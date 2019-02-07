using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private PlayerManager playerManager;
    private int roundNumber = 1;
    private int playerAmount;
    private List<PlayerInput> playerInputs = new List<PlayerInput>();
    [SerializeField] private int holdDirectionTime = 2;
    private List<Move> moves = new List<Move>();


    private void Start()
    {
        GameManager.GM.StartGame(this);
        playerManager = GameManager.GM.PManager();
        playerAmount = GameManager.GM.PlayerAmount();

        for (int i = 0; i < playerAmount; i++)
        {
            playerInputs.Add(gameObject.AddComponent<PlayerInput>());
            playerInputs[i].turnManager = this;
            playerInputs[i].playerNumberInt = (i + 1);
            playerInputs[i].secondsTillSelect = holdDirectionTime;
            playerInputs[i].GetInput();
        }
    }

    public void StartRound() // FIGURE OUT, placed above
    {
        Debug.Log("Start Round");
        for (int i = 0; i < playerInputs.Count; i++)//foreach (PlayerInput p in playerInputs)
        {
            
        }
    }

    public void AddPlayerMoves(List<Move> moveList)
    {
        moves.AddRange(moveList);
        if (moves.Count == (playerAmount * 3))
        {
            EnterCombat();
        }
    }

    private void EnterCombat()
    {
        
        for (int moveNo = 1; moveNo < 5; moveNo++) //Go through all the moves made first, then second etc    
        {
            for (int moveType = 1; moveType < 2; moveType++) //Go through moves before shoots   
            {
                for (int i = 0; i < moves.Count; i++) // Go through list of moves
                {
                    if (moves[i].moveNumber == moveNo)
                    {
                        if (moves[i].moveType == moveNo && moveType == 1) // Will go through first made moves, then moves not shoots
                        {
                            MakeMove(i);
                        }
                        else if (moves[i].moveType == moveNo && moveType == 2)
                        {
                            MakeMove(i);
                        }
                    }
                }

            }
            // Next round!
        }
    }

    private void MakeMove(int index)// moves[index]
    {
        if (moves[index].moveType == 1)
        {
            playerManager.MovePlayer(index, 1);
        }
        else if (moves[index].moveType == 2)
        {
            //playerManager
        }
    }

}
