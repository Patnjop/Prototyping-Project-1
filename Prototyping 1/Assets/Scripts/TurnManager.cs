using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class TurnManager : MonoBehaviour
{
    public PlayerManager playerManager;
    private int roundNumber = 1;
    private int playerAmount;
    private List<PlayerInput> playerInputs = new List<PlayerInput>();
    [SerializeField] private int holdDirectionTime = 2;
    private List<Move> moves = new List<Move>();
    private Queue<Move> movesQueue = new Queue<Move>();
    public GameObject[] playerTurnInfo;
    public UIFields[] playerCharacterCards;
    public Sprite[] heads;
    public int movesPerTurn;
    private int readyPlacement;




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
            playerTurnInfo[i].SetActive(true);


        }
    }

    public void StartRound() // needs playerAmount?
    {
        readyPlacement = 0;
        List<Player> players = new List<Player>(GameManager.GM.GetPlayers());
        foreach (Player p in players)
        {
            p.turnPlacement = 0;
        }
        RefreshCharacterCards();
        moves = new List<Move>();
        playerAmount = playerManager.PlayerAmount();
        for (int i = 0; i < playerAmount; i++)
        {
            if (playerManager.PlayerAlive(i + 1))
            {
                playerInputs[i].GetInput();
                playerTurnInfo[i].SetActive(true);
                playerTurnInfo[i].GetComponentInChildren<TextMeshProUGUI>().text = "0/3";
            }


        }
    }

    public void FillCircle(int playerNum, float amount, float max)
    {
        playerTurnInfo[playerNum - 1].GetComponentInChildren<Image>().fillAmount = (amount / max);

    }

    public void IncrementInfo(int playerNum, int amount)
    {
        playerTurnInfo[playerNum - 1].GetComponentInChildren<TextMeshProUGUI>().text = amount + "/3";

    }

    public void AddPlayerMoves(List<Move> moveList, int playerNum)
    {
        List<Player> players = new List<Player>(GameManager.GM.GetPlayers());
        readyPlacement += 1;
        players[playerNum - 1].turnPlacement = readyPlacement;
        RefreshCharacterCards();

        playerTurnInfo[playerNum - 1].GetComponentInChildren<TextMeshProUGUI>().text = "Ready!";
        moves.AddRange(moveList);
        moves = moves.OrderBy(o => o.moveNumber).ToList();

        if (moves.Count == (playerAmount * movesPerTurn))
        {
            foreach (GameObject game in playerTurnInfo)
            {
                game.SetActive(false);
            }
            movesQueue = new Queue<Move>(moves);
            EnterCombat();
        }
    }

    public void EnterCombat(bool bulletsCleared = false)
    {
        Debug.Log("Bullets cleared = " + bulletsCleared);
        if ((movesQueue.Count == (playerAmount * movesPerTurn) - playerAmount
            || movesQueue.Count == (playerAmount * movesPerTurn) - playerAmount * 2
            || movesQueue.Count == (playerAmount * movesPerTurn) - playerAmount * 3) && bulletsCleared == false)
        {
            playerManager.CheckCombatChapter();
        }
        else if (movesQueue.Count > 0)
        {
            if (movesQueue.Peek().special == true)
            {
                playerManager.SetSpecial(movesQueue.Peek().player);
            }
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

    public void RefreshCharacterCards(int placement = 0, int characterCardAmount = 0)
    {
        if (characterCardAmount > 0)
        {
            for(int i = 0; i < characterCardAmount; i++)
            {
                playerCharacterCards[i].characterCard.SetActive(true);
            }
        }

        List<Player> players = new List<Player>(GameManager.GM.GetPlayers());

        for (int i = 0; i < players.Count; i++)
        { 
            playerCharacterCards[i].nameUI.text = players[i].name;
            playerCharacterCards[i].ammoUI.text = players[i].ammo.ToString();
            playerCharacterCards[i].headUI.sprite = heads[players[i].head];
            if (players[i].turnPlacement != 0)
            {
                playerCharacterCards[i].placementUI.text = players[i].turnPlacement.ToString() + ". " + players[i].name;
            }
            else
            {
                playerCharacterCards[i].placementUI.text = "";
            }
            if (players[i].alive == false)
            {
                playerCharacterCards[i].deadX.SetActive(true);
            }
        }
    }

}
