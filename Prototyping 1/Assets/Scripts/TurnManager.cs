using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class TurnManager : MonoBehaviour
{
    private PlayerManager playerManager;
    private int roundNumber = 1;
    private int playerAmount;
    private List<PlayerInput> playerInputs = new List<PlayerInput>();
    [SerializeField] private int holdDirectionTime = 2;
    private List<Move> moves = new List<Move>();
    private Queue<Move> movesQueue = new Queue<Move>();
    public GameObject inputInfo, worldCanvas;
    private List<GameObject> infoObjectList = new List<GameObject>();




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

        }

        StartNewGame();

    }

    public void StartNewGame()
    {
        

        for (int i = 0; i < playerAmount; i++)
        {

            playerInputs[i].GetInput();
            infoObjectList.Add(Instantiate(inputInfo, playerManager.GetPlayerPos(i), Quaternion.identity, worldCanvas.transform));

        }
    }

    public void StartRound() // needs playerAmount?
    {
        moves = new List<Move>();
        
        for (int i = 0; i < playerAmount; i++)
        {
            
            
            if (infoObjectList.Count == 0)
            {
                playerInputs[i].GetInput();
                infoObjectList.Add(Instantiate(inputInfo, playerManager.GetPlayerPos(i), Quaternion.identity, worldCanvas.transform));
            }
            else if (playerManager.PlayerAlive(i + 1) && infoObjectList.Count > 0)
            {
                playerInputs[i].GetInput();
                infoObjectList[i].SetActive(true);
                infoObjectList[i].transform.position = playerManager.GetPlayerPos(i);
                infoObjectList[i].GetComponentInChildren<TextMeshProUGUI>().text = "0/3";
            }

        }
    }

    public void FillCircle(int playerNum, float amount, float max)
    {
        infoObjectList[playerNum-1].GetComponentInChildren<Image>().fillAmount = (amount / max);
    }

    public void IncrementInfo(int playerNum, int amount)
    {
        infoObjectList[playerNum-1].GetComponentInChildren<TextMeshProUGUI>().text = amount + "/3";
    }

    public void AddPlayerMoves(List<Move> moveList, int playerNum)
    {
        infoObjectList[playerNum - 1].GetComponentInChildren<TextMeshProUGUI>().text = "Ready!";
        Debug.Log("Pre-sort, Moves count: " + moves.Count);
        moves.AddRange(moveList);
        moves = moves.OrderBy(o => o.moveNumber).ThenBy(c => c.moveType).ToList();
        
        if (moves.Count == (playerAmount * 3))
        {
            foreach (GameObject game in infoObjectList)
            {
                game.SetActive(false);
            }
            movesQueue = new Queue<Move>(moves);
            EnterCombat();
        }
    }

    public void EnterCombat()
    {
        if (movesQueue.Count > 0)
        {
            MakeMove(movesQueue.Dequeue());
        }
        else
        {
            StartRound();
        }
        
  
    }

    private void MakeMove(Move move)// moves[index]
    {
        if (playerManager.PlayerAlive(move.player))
        {
            if (move.moveType == 1)
            {
                playerManager.MovePlayer(move.player, move.direction);
            }
            else if (move.moveType == 2)
            {
                playerManager.ShootPlayer(move.player, move.direction);
            }
        }
    }

}
