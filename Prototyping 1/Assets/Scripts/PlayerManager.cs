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
            GameObject Player = Instantiate(p.character, spawnLocation[p.playerNumber - 1], Quaternion.identity);
            activePlayers.Add(Player);
        }
        players = GameManager.GM.AddInstantiatedCharacters(activePlayers);
        Debug.Log("Called");
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

    public void MovePlayer(int p, int dir) // p = player number
    {
        Debug.Log("Moving player: " + p + " in direction " + dir);
        Debug.Log("Player " + 1 + " x,y: " + players[0].character.transform.position.x + "," + players[0].character.transform.position.y);
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].playerNumber == p)
            {
                Transform direction = players[i].character.transform;
                if (dir == 1)
                {
                    if (players[i].character.transform.position.y != 0)
                    {
                        direction.position = new Vector2(direction.position.x, direction.position.y + 1);
                        bool spaceTaken;
                        spaceTaken = GridSpaceTaken(direction);
                        if (!spaceTaken)
                        {
                            players[i].character.transform.position = new Vector2(players[i].character.transform.position.x,
                            players[i].character.transform.position.y + 1);

                            spaceTaken = AmmoInSpace(direction);
                            if (spaceTaken)
                            {
                                //AMMO UP
                            }
                        }
                        else
                        {
                            Debug.Log("Player 1 blocked by item");
                            //BLOCKED
                        }
                    }
                    else
                    {
                        Debug.Log("Player 1 blocked by end map");
                        //BLOCKED
                    }
                }
                else if (dir == 2)
                {
                    if (players[i].character.transform.position.x != GameManager.GM.gridSize.x)
                    {
                        direction.position = new Vector2(direction.position.x + 1, direction.position.y);
                        bool spaceTaken;
                        spaceTaken = GridSpaceTaken(direction);
                        if (!spaceTaken)
                        {
                            players[i].character.transform.position = new Vector2(players[i].character.transform.position.x + 1,
                            players[i].character.transform.position.y);

                            spaceTaken = AmmoInSpace(direction);
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
                    if (players[i].character.transform.position.y != GameManager.GM.gridSize.y)
                    {
                        direction.position = new Vector2(direction.position.x, direction.position.y - 1);
                        bool spaceTaken;
                        spaceTaken = GridSpaceTaken(direction);
                        if (!spaceTaken)
                        {
                            players[i].character.transform.position = new Vector2(players[i].character.transform.position.x,
                            players[i].character.transform.position.y - 1);

                            spaceTaken = AmmoInSpace(direction);
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
                    if (players[i].character.transform.position.x != 0)
                    {
                        direction.position = new Vector2(direction.position.x - 1, direction.position.y);
                        bool spaceTaken;
                        spaceTaken = GridSpaceTaken(direction);
                        if (!spaceTaken)
                        {
                            players[i].character.transform.position = new Vector2(players[i].character.transform.position.x - 1,
                            players[i].character.transform.position.y);

                            spaceTaken = AmmoInSpace(direction);
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

            }


        }
        Invoke("NextStep", 0.5f);
    }

    private void NextStep()
    {
        TurnManager.EnterCombat();
    }

    private void CheckBulletPath(int dir, Transform check) //the direction, position
    {
        float posX = check.position.x;
        float posY = check.position.y;
        int length = 0;
        if (dir == 1)
        {
            length = (int)(0 - posY);
        }
        else if (dir == 2)
        {
            length = (int)(GameManager.GM.gridSize.x - posX);
        }
        else if (dir == 3)
        {
            length = (int)((GameManager.GM.gridSize.y * -1) + posY);
        }
        else if (dir == 4)
        {
            length = (int)(0 + posX);
        }

        for (int i = 1; i < length; i++)
        {
            if (dir == 1)
            {
                for (int p = 0; p < players.Count; p++)
                {
                    if ((posY + i) == players[p].character.transform.position.y && posX == players[p].character.transform.position.x)
                    {
                        //Dead
                    }
                }
            }
            else if (dir == 2)
            {
                for (int p = 0; p < players.Count; p++)
                {
                    if ((posX + i) == players[p].character.transform.position.x && posY == players[p].character.transform.position.y)
                    {
                        //Dead
                    }
                }
            }
            else if (dir == 3)
            {
                for (int p = 0; p < players.Count; p++)
                {
                    if ((posY - i) == players[p].character.transform.position.x && posY == players[p].character.transform.position.y)
                    {
                        //Dead
                    }
                }
            }
            else if (dir == 4)
            {
                for (int p = 0; p < players.Count; p++)
                {
                    if ((posX - i) == players[p].character.transform.position.x && posY == players[p].character.transform.position.y)
                    {
                        //Dead
                    }
                }
            }
        }
    }

    public void ShootPlayer(int p, int dir)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].playerNumber == p)
            {
                CheckBulletPath(dir, players[i].character.transform);
            }
        }

        Invoke("NextStep", 0.5f);
    }

    public Vector2 GetPlayerPos(int playerNum)
    {
        return players[playerNum].character.transform.position;
    }


}
