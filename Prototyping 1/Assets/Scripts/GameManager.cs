using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    [SerializeField] Vector2 gridSize;
    private PlayerManager PlayerManager;
    private int playerAmount;
    [SerializeField] private List<Player> players;

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
        PlayerManager = GetComponent<PlayerManager>();
    }

    public void SetGameData(List<Player> playerList) //Done in character creator
    {
        players = playerList;
        playerAmount = players.Count;
    }

    public List<Player> AddInstantiatedCharacters(List<GameObject> characterList)
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            players[i].character = characterList[i];
        }
        return players;
    }

    public void StartGame(TurnManager turnManager)
    {
        PlayerManager.TurnManager = turnManager;
        PlayerManager.SpawnPlayers(players);
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
