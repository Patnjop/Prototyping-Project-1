using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<Vector2> spawnLocation;
    private List<Player> players = new List<Player>();
    [SerializeField] private GameObject map;
    public TurnManager TurnManager;
    private List<GameObject> bullets = new List<GameObject>(); // maybe make class??

    public void SpawnPlayers(List<Player> playerList)
    {
        List<GameObject> activePlayers = new List<GameObject>();

        foreach (Player p in playerList)
        {
            GameObject Player = Instantiate(p.character, GridCalculator.PointsToGrid(spawnLocation[p.playerNumber - 1]), Quaternion.identity, map.transform);
            activePlayers.Add(Player);
        }
        players = GameManager.GM.AddInstantiatedCharacters(activePlayers);

        TurnManager.StartRound();
    }

    private bool GridSpaceTaken(Transform check)
    {
        for (int i = 0; i < players.Count; i++) //Add objects
        {
            if (check.position == players[i].character.transform.position)
            {
                return true;
            }
        }
        return false;
    }

    private bool BulletInSpace(Transform check)
    {
        for (int i = 0; i < bullets.Count; i++) //Add objects
        {
            if (check.position == bullets[i].transform.position)
            {
                return true;
            }
        }
        return false;
    }

    private bool AmmoInSpace(Transform check) // TURN INTO AMMO
    {
        for (int i = 0; i < bullets.Count; i++) //Add objects
        {
            if (check.position == bullets[i].transform.position)
            {
                return true;
            }
        }
        return false;
    }

    public void MovePlayer(int p, int dir)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].playerNumber == p)
            {
                if (dir == 1)
                {
                    if (players[i].character.transform.position.y != 4.5)
                    {
                        bool spaceTaken;
                        spaceTaken = GridSpaceTaken(players[i].character.transform);
                        if (!spaceTaken)
                        {
                            players[i].character.transform.position = new Vector2(players[i].character.transform.position.x,
                            players[i].character.transform.position.y + 1);
                            spaceTaken = BulletInSpace(players[i].character.transform);
                            if (spaceTaken)
                            {
                                //DIE
                            }
                            spaceTaken = AmmoInSpace(players[i].character.transform);
                            if (spaceTaken)
                            {
                                //AMMO UP
                            }
                        }
                        else
                        {
                            //BLOCKED
                        }
                    }
                    else
                    {
                        //BLOCKED
                    }
                }
                else if (dir == 2)
                {
                    if (players[i].character.transform.position.x != 4.5)
                    {
                        bool spaceTaken;
                        spaceTaken = GridSpaceTaken(players[i].character.transform);
                        if (!spaceTaken)
                        {
                            players[i].character.transform.position = new Vector2(players[i].character.transform.position.x + 1,
                            players[i].character.transform.position.y);
                            spaceTaken = BulletInSpace(players[i].character.transform);
                            if (spaceTaken)
                            {
                                //DIE
                            }
                            spaceTaken = AmmoInSpace(players[i].character.transform);
                            if (spaceTaken)
                            {
                                //AMMO UP
                            }
                        }
                        else
                        {
                            //BLOCKED
                        }
                    }
                    else
                    {
                        //BLOCKED
                    }
                }
                else if (dir == 3)
                {
                    if (players[i].character.transform.position.y != -4.5)
                    {
                        bool spaceTaken;
                        spaceTaken = GridSpaceTaken(players[i].character.transform);
                        if (!spaceTaken)
                        {
                            players[i].character.transform.position = new Vector2(players[i].character.transform.position.x,
                            players[i].character.transform.position.y - 1);
                            spaceTaken = BulletInSpace(players[i].character.transform);
                            if (spaceTaken)
                            {
                                //DIE
                            }
                            spaceTaken = AmmoInSpace(players[i].character.transform);
                            if (spaceTaken)
                            {
                                //AMMO UP
                            }
                        }
                        else
                        {
                            //BLOCKED
                        }
                    }
                    else
                    {
                        //BLOCKED
                    }
                }
                else if (dir == 4)
                {
                    if (players[i].character.transform.position.x != -4.5)
                    {
                        bool spaceTaken;
                        spaceTaken = GridSpaceTaken(players[i].character.transform);
                        if (!spaceTaken)
                        {
                            players[i].character.transform.position = new Vector2(players[i].character.transform.position.x - 1,
                            players[i].character.transform.position.y);
                            spaceTaken = BulletInSpace(players[i].character.transform);
                            if (spaceTaken)
                            {
                                //DIE
                            }
                            spaceTaken = AmmoInSpace(players[i].character.transform);
                            if (spaceTaken)
                            {
                                //AMMO UP
                            }
                        }
                        else
                        {
                            //BLOCKED
                        }
                    }
                    else
                    {
                        //BLOCKED
                    }
                }
                break;
            }
            
        }
    }

    public void ShootPlayer(int p, int dir)
    {

    }


}
