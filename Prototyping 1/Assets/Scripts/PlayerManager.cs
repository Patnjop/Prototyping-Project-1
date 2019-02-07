using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<Vector2> spawnLocation;
    private List<GameObject> activePlayers = new List<GameObject>();
    [SerializeField] private GameObject map;
    public TurnManager TurnManager;

    public void SpawnPlayers(List<Player> playerList)
    {
        foreach (Player p in playerList)
        {
            GameObject Player = Instantiate(p.character, GridCalculator.PointsToGrid(spawnLocation[p.playerNumber - 1]), Quaternion.identity, map.transform);

            activePlayers.Add(Player);
        }


    }
}
