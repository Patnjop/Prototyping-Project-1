using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager GM = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public Vector2 gridSize;
    private PlayerManager PlayerManager;
    public PlayerManager storedPlayerManager;
    private int playerAmount;
    [SerializeField] private List<Player> players;
    [SerializeField] private List<Player> storedPlayers;
    public bool firstGame = true;
    public UIManager UIManager;
    public int[] playerScores;


    //[SerializeField] private bool debugMode;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (GM == null)
        {
            //if not, set instance to this
            GM = this;
        }
        //If instance already exists and it's not this:
        else if (GM != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        PlayerManager = GetComponentInChildren<PlayerManager>();


    }

    public void SetGameData(List<Player> playerList) //Done in character creator
    {
        players = playerList;
        playerAmount = players.Count;
        playerScores = new int[playerAmount];
        players = players.OrderBy(o => o.playerNumber).ToList();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public List<Player> AddInstantiatedCharacters(List<GameObject> characterList)
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            players[i].character = characterList[i];
        }
        if (storedPlayers.Count == 0)
        {
            SetStored();
        }
        return players;
    }

    public List<Player> GetPlayers()
    {
        return players;
    }

    private void SetStored()
    {
        foreach (Player p in players)
        {
            Debug.Log("Clone");
            storedPlayers.Add(new Player(p.playerNumber, p.name, p.head, p.body, p.playerClass, p.character));
        }

    }

    public void StartGame(TurnManager turnManager)
    {
        Debug.Log("Start Game was called");
        if (gameObject.transform.childCount == 0)
        {
            Instantiate(storedPlayerManager, gameObject.transform);
            PlayerManager = GetComponentInChildren<PlayerManager>();
        }

        PlayerManager.TurnManager = turnManager;

        PlayerManager.SpawnPlayers(players);
        /*foreach (Player p in players)
        {
            p.character.SetActive(true);
        }*/
        //UIManager.SetScores(playerScores);
    }

    public void NewGame()
    {
        bool gameOver = false;
        int winnerNum = 0;
        foreach (Player p in players)
        {
            playerScores[p.playerNumber - 1] += p.points;
            if (p.points == 4)
            {
                gameOver = true;
                winnerNum = p.playerNumber;
            }
        }
        if (!gameOver)
        {/*
            firstGame = false;
            players = storedPlayers;
            Destroy(gameObject.transform.GetChild(0).gameObject);
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            UIManager.SetScores(playerScores);*/
        }
        else
        {
            UIManager.SetWinner(winnerNum);
        }
    }

    public int PlayerAmount()
    {
        return players.Count;
    }

    public PlayerManager PManager()
    {
        return PlayerManager;
    }
}
