using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<Vector2> spawnLocation;
    private List<Player> players = new List<Player>();
    [SerializeField] private GameObject map;
    public TurnManager TurnManager;
    private List<Bullet> bullets = new List<Bullet>();
    private List<GameObject> ammo = new List<GameObject>();
    [SerializeField] private int aliveCount;
    [SerializeField] private GameObject bulletGO;

    public void SpawnPlayers(List<Player> playerList)
    {
        List<GameObject> activePlayers = new List<GameObject>();
        Debug.Log("Spawning Players");
        foreach (Player p in playerList)
        {
            GameObject Player = Instantiate(p.character, spawnLocation[p.playerNumber - 1], Quaternion.identity);
            activePlayers.Add(Player);
        }
        players = GameManager.GM.AddInstantiatedCharacters(activePlayers);
        aliveCount = players.Count;
        TurnManager.StartNewGame();
    }

    private bool GridSpaceTaken(Transform check)
    {
        for (int i = 0; i < players.Count; i++) //Add objects
        {
            if (check.position == players[i].character.transform.position && players[i].alive)
            {
                return true;
            }
        }
        return false;
    }

    private bool AmmoInSpace(Transform check) // TURN INTO AMMO
    {
        
        for (int i = 0; i < ammo.Count; i++) //Add objects
        {
            if (check.position == ammo[i].transform.position)
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
                            Debug.Log("Player 1 blocked by item or dead");
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
        UpdateBullets();
        Invoke("NextStep", 0.5f);
    }

    private void NextStep()
    {

        if (aliveCount > 1)
        {

            TurnManager.EnterCombat();
        }
        else // FIX
        {
            Debug.Log("New Game!");

            GameManager.GM.NewGame();
        }
    }

    public void CheckCombatChapter()
    {
        
        if (bullets.Count > 0)
        {
            UpdateBullets();
            Invoke("NextStep", 0.5f);
        }
        else
        {
            Debug.Log("Checking Chapter");
            foreach(Bullet b in bullets)
            {
                Destroy(b.body);
                bullets.Remove(b);
            }
            StartCoroutine("ChapterDoneDelay", 0.5f);
        }
    }

    IEnumerator ChapterDoneDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        TurnManager.EnterCombat(true);
    }



    private void KillPlayer(int playerNum)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].playerNumber == playerNum)
            {
                players[i].alive = false;
                players[i].character.SetActive(false);
                aliveCount -= 1;
            }
        }
    }

    private void UpdateBullets() //the direction, position
    {
        Bullet toRemove = new Bullet(0,0);
        bool removeBullet = false;
        foreach (Bullet b in bullets)
        {
            if (b.direction == 1)
            {
                b.body.transform.position = new Vector2(b.body.transform.position.x, b.body.transform.position.y + 1);
                for (int p = 0; p < players.Count; p++)
                {
                    if ((b.body.transform.position.y) == players[p].character.transform.position.y && b.body.transform.position.x == players[p].character.transform.position.x && players[p].alive)
                    {
                        KillPlayer(players[p].playerNumber);
                        toRemove = b;
                        removeBullet = true;
                    }
                }
            }
            else if (b.direction == 2)
            {
                b.body.transform.position = new Vector2(b.body.transform.position.x + 1, b.body.transform.position.y);
                for (int p = 0; p < players.Count; p++)
                {
                    if ((b.body.transform.position.x) == players[p].character.transform.position.x && b.body.transform.position.y == players[p].character.transform.position.y && players[p].alive)
                    {
                        KillPlayer(players[p].playerNumber);
                        toRemove = b;
                        removeBullet = true;
                    }
                }
            }
            else if (b.direction == 3)
            {
                b.body.transform.position = new Vector2(b.body.transform.position.x, b.body.transform.position.y - 1);
                for (int p = 0; p < players.Count; p++)
                {
                    if ((b.body.transform.position.y) == players[p].character.transform.position.y && b.body.transform.position.x == players[p].character.transform.position.x && players[p].alive)
                    {
                        KillPlayer(players[p].playerNumber);
                        toRemove = b;
                        removeBullet = true;
                    }
                }
            }
            else if (b.direction == 4)
            {
                b.body.transform.position = new Vector2(b.body.transform.position.x - 1, b.body.transform.position.y);
                for (int p = 0; p < players.Count; p++)
                {

                    if ((b.body.transform.position.x) == players[p].character.transform.position.x && b.body.transform.position.y == players[p].character.transform.position.y && players[p].alive)
                    {
                        KillPlayer(players[p].playerNumber);
                        toRemove = b;
                        removeBullet = true;
                    }

                }
            }
            b.movesLeft -= 1;
            if (b.movesLeft == 0)
            {
                toRemove = b;
                removeBullet = true;
            }
        }

        if (removeBullet)
        {
            Destroy(toRemove.body);
            bullets.Remove(toRemove);
        }
    }

    private void MakeBullet(int dir, Transform check)
    {
        float posX = check.position.x;
        float posY = check.position.y;
        Vector2 spawnPos = new Vector2();
        int length = 0;
        if (dir == 1)
        {
            length = (int)(0 - posY);
            spawnPos = new Vector2(posX, posY+1);
        }
        else if (dir == 2)
        {
            length = (int)(GameManager.GM.gridSize.x - posX);
            spawnPos = new Vector2(posX + 1, posY);
        }
        else if (dir == 3)
        {
            length = (int)((GameManager.GM.gridSize.y * -1) + posY);
            spawnPos = new Vector2(posX, posY - 1);
        }
        else if (dir == 4)
        {
            length = (int)(0 + posX);
            spawnPos = new Vector2(posX - 1, posY);
        }
        UpdateBullets();
        bullets.Add(new Bullet(dir, length));
        bullets[bullets.Count - 1].body = Instantiate(bulletGO, spawnPos, Quaternion.identity);
    }

    public void ShootPlayer(int p, int dir)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].playerNumber == p)
            {
                MakeBullet(dir, players[i].character.transform);
            }
        }

        Invoke("NextStep", 0.5f);
    }

    public Vector2 GetPlayerPos(int playerNum) // for UI
    {
        return players[playerNum].character.transform.position;
    }

    public bool PlayerAlive(int p)
    {
        
        for (int i = 0; i < players.Count; i++)
        {

            if (players[i].playerNumber == p && players[i].alive == true)
            {

                return true;
            }
            
        }
        return false;
    }

    public int PlayerAmount()
    {
        return aliveCount;
    }


}
