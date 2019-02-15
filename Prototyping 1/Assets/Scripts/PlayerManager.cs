using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<Vector2> spawnLocation;
    private List<Player> players = new List<Player>();
    [SerializeField] private GameObject map;
    public TurnManager TurnManager;
    public List<Bullet> bullets = new List<Bullet>();
    [SerializeField] private int aliveCount;
    [SerializeField] private GameObject bulletGO;
    public GameObject bulletManager;
    public List<Transform> ammoPiles;
    public int ammoToGain;
    [SerializeField] GameObject deathFX, acidFX, TBulletFX, ammoFX, acidTile;

    [SerializeField] private GameObject playerObject;
    [SerializeField] private Color[] playerColours;

    [SerializeField] AudioSource moveSound, gunShotSound, deathSound, ammoPickupSound;

    public void SpawnPlayers(List<Player> playerList)
    {
        if (GameManager.GM.firstGame)
        {
            List<GameObject> activePlayers = new List<GameObject>();

            foreach (Player p in playerList)
            {

                p.character = Instantiate(playerObject, spawnLocation[p.playerNumber - 1], Quaternion.identity) as GameObject;
                p.character.SetActive(true);
                activePlayers.Add(p.character);
                p.character.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = TurnManager.heads[p.head];

                if (p.body == 0)
                {
                    p.character.transform.GetChild(0).gameObject.SetActive(true);
                    p.character.transform.GetChild(0).GetComponent<SpriteRenderer>().color = playerColours[p.playerNumber - 1];
                }
                else if (p.body == 1)
                {
                    p.character.transform.GetChild(3).gameObject.SetActive(true);
                    p.character.transform.GetChild(3).GetComponent<SpriteRenderer>().color = playerColours[p.playerNumber - 1];
                }
                else if (p.body == 2)
                {
                    p.character.transform.GetChild(4).gameObject.SetActive(true);
                    p.character.transform.GetChild(4).GetComponent<SpriteRenderer>().color = playerColours[p.playerNumber - 1];
                }


            }
            players = GameManager.GM.AddInstantiatedCharacters(activePlayers);
            aliveCount = players.Count;
            TurnManager.RefreshCharacterCards(0, aliveCount);
        }
        else
        {
            aliveCount = GameManager.GM.PlayerAmount();
            players = GameManager.GM.GetPlayers();
        }
        foreach (Bullet b in bullets)
        {
            Destroy(b.body);

        }

        TurnManager.StartNewGame();
    }

    private bool GridSpaceTaken(Transform check, int p)
    {
        for (int i = 0; i < players.Count; i++) //Add objects
        {
            if (check.position == players[i].character.transform.position && players[i].alive && p != players[i].playerNumber)
            {
                KillPlayer(players[i].playerNumber);
                return false;
            }
        }
        return false;
    }

    private void AmmoInSpace(Transform check, int playerNum) // TURN INTO AMMO
    {
        int toDestroy = -1;
        for (int i = 0; i < ammoPiles.Count; i++) //Add objects
        {
            if (check.position == ammoPiles[i].position)
            {
                PlayFX(ammoFX, check);
                players[playerNum].ammo += ammoToGain;
                Destroy(ammoPiles[i].gameObject);
                toDestroy = i;
                ammoPickupSound.Play();
            }
        }
        if (toDestroy != -1)
        {
            ammoPiles.Remove(ammoPiles[toDestroy]);
        }

    }



    private bool HitBullet(Transform check)
    {
        for (int i = 0; i < bullets.Count; i++) //Add objects
        {
            if (check.position == bullets[i].body.transform.position)
            {

                return true;
            }
        }
        return false;
    }

    public void MovePlayer(int p, int dir, bool dontUpdate = false) // p = player number
    {
        moveSound.Play();
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
                        spaceTaken = GridSpaceTaken(direction, p);
                        if (HitBullet(players[i].character.transform))
                        {
                            KillPlayer(players[i].playerNumber);
                        }
                        AmmoInSpace(direction, i);



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
                        if (HitBullet(players[i].character.transform))
                        {
                            KillPlayer(players[i].playerNumber);
                        }
                        spaceTaken = GridSpaceTaken(direction, p);
                        AmmoInSpace(direction, i);


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
                        if (HitBullet(players[i].character.transform))
                        {
                            KillPlayer(players[i].playerNumber);
                        }
                        spaceTaken = GridSpaceTaken(direction, p);
                        AmmoInSpace(direction, i);
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
                        if (HitBullet(players[i].character.transform))
                        {
                            KillPlayer(players[i].playerNumber);
                        }
                        spaceTaken = GridSpaceTaken(direction, p);
                        AmmoInSpace(direction, i);
                    }
                    else
                    {
                        //BLOCKED
                    }
                }

            }


        }
        if (!dontUpdate)
        {
            UpdateBullets();
        }
        Invoke("NextStep", 0.5f);
    }

    private void NextStep()
    {

        if (aliveCount > 1)
        {
            foreach (Player p in players)
            {
                p.special = false;
            }
            TurnManager.EnterCombat();
        }
        else // FIX
        {
            Debug.Log("New Game!");
            foreach (Player p in players)
            {
                if (p.alive)
                {
                    p.points += 4;
                }
            }
            GameManager.GM.NewGame();
        }
    }

    public void CheckCombatChapter()
    {
        if (aliveCount > 1)
        {


            if (bullets.Count > 0)
            {
                UpdateBullets();
                Invoke("NextStep", 0.5f);
            }
            else
            {
                Debug.Log("Checking Chapter");
                foreach (Bullet b in bullets)
                {
                    Destroy(b.body);
                    bullets.Remove(b);
                }
                /*foreach (Transform t in bulletManager.transform)
                {
                    Destroy(t.gameObject);
                }*/
                StartCoroutine("ChapterDoneDelay", 0.5f);
            }
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
                PlayFX(deathFX, players[i].character.transform);
                players[i].alive = false;
                players[i].character.SetActive(false);
                deathSound.Play();

                aliveCount -= 1;
                if (aliveCount == 2)
                {
                    players[i].points += 1;
                }
                else if (aliveCount == 1)
                {
                    players[i].points += 2;
                }



            }
        }
    }

    private void UpdateBullets() //the direction, position
    {


        List<Bullet> toRemove = new List<Bullet>();
        bool removeBullet = false;
        bool splitBullet = false;
        List<Bullet> splitIndex = new List<Bullet>();
        List<Bullet> fireIndex = new List<Bullet>();

        foreach (Bullet b in bullets)
        {
            b.movesLeft -= 1;
            if (b.direction == 1)
            {
                if (b.type == 2 && b.body.transform.position.y >= 0)
                {
                    b.body.transform.position = new Vector2(b.body.transform.position.x, GameManager.GM.gridSize.y);
                }
                else
                {

                    b.body.transform.position = new Vector2(b.body.transform.position.x, b.body.transform.position.y + 1);
                }
                for (int p = 0; p < players.Count; p++)
                {
                    if (b.movesLeft != 0 && (b.body.transform.position.y) == players[p].character.transform.position.y && b.body.transform.position.x == players[p].character.transform.position.x && players[p].alive)
                    {
                        KillPlayer(players[p].playerNumber);
                        toRemove.Add(b);
                        removeBullet = true;
                    }
                }
            }
            else if (b.direction == 2)
            {
                if (b.type == 2 && b.body.transform.position.x >= GameManager.GM.gridSize.x)
                {
                    b.body.transform.position = new Vector2(0, b.body.transform.position.y);
                }
                else
                {

                    b.body.transform.position = new Vector2(b.body.transform.position.x + 1, b.body.transform.position.y);
                }
                for (int p = 0; p < players.Count; p++)
                {
                    if (b.movesLeft != 0 && (b.body.transform.position.x) == players[p].character.transform.position.x && b.body.transform.position.y == players[p].character.transform.position.y && players[p].alive)
                    {
                        KillPlayer(players[p].playerNumber);
                        toRemove.Add(b);
                        removeBullet = true;
                    }
                }
            }
            else if (b.direction == 3)
            {
                if (b.type == 2 && b.body.transform.position.y <= GameManager.GM.gridSize.y)
                {
                    b.body.transform.position = new Vector2(b.body.transform.position.x, 0);
                }
                else
                {

                    b.body.transform.position = new Vector2(b.body.transform.position.x, b.body.transform.position.y - 1);
                }
                for (int p = 0; p < players.Count; p++)
                {
                    if (b.movesLeft != 0 && (b.body.transform.position.y) == players[p].character.transform.position.y && b.body.transform.position.x == players[p].character.transform.position.x && players[p].alive)
                    {
                        KillPlayer(players[p].playerNumber);
                        toRemove.Add(b);
                        removeBullet = true;
                    }
                }
            }
            else if (b.direction == 4)
            {
                if (b.type == 2 && b.body.transform.position.x <= 0)
                {
                    b.body.transform.position = new Vector2(GameManager.GM.gridSize.x, b.body.transform.position.y);
                }
                else
                {

                    b.body.transform.position = new Vector2(b.body.transform.position.x - 1, b.body.transform.position.y);
                }
                for (int p = 0; p < players.Count; p++)
                {

                    if (b.movesLeft != 0 && (b.body.transform.position.x) == players[p].character.transform.position.x && b.body.transform.position.y == players[p].character.transform.position.y && players[p].alive)
                    {
                        KillPlayer(players[p].playerNumber);
                        toRemove.Add(b);
                        removeBullet = true;
                    }

                }
            }

            //Debug.Log("Moves Left = " + b.movesLeft);
            if (b.type == 1)
            {
                fireIndex.Add(b);
            }
            if (b.type == 4 && b.movesLeft < 1)
            {
                splitBullet = true;
                splitIndex.Add(b);
                toRemove.Add(b);
                removeBullet = true;
            }
            else if (b.movesLeft < 1)
            {
                toRemove.Add(b);
                removeBullet = true;
            }
        }
        if (fireIndex.Count > 0)
        {
            foreach (Bullet f in fireIndex)
            {
                PlayFX(acidFX, f.body.transform);
                MakeBullet(0, new Vector2(f.body.transform.position.x, f.body.transform.position.y), 0, 1);
            }

        }
        if (splitBullet)
        {
            foreach (Bullet s in splitIndex)
            {
                PlayFX(TBulletFX, s.body.transform);
                MakeBullet(GetTShotDir(s.direction, 1), new Vector2(s.body.transform.position.x, s.body.transform.position.y), 0, 2);
                MakeBullet(GetTShotDir(s.direction, 2), new Vector2(s.body.transform.position.x, s.body.transform.position.y), 0, 2);
            }
        }
        if (removeBullet)
        {
            foreach (Bullet r in toRemove)
            {
                Destroy(r.body);
                bullets.Remove(r);
            }
        }
    }

    private void MakeBullet(int dir, Vector2 check, int playerNum = 0, int range = 0) // dont use range 1
    {
        float posX = check.x;
        float posY = check.y;
        Vector2 spawnPos = new Vector2();
        int length = 0;

        if (dir == 1)
        {
            length = (int)(0 - posY);
            spawnPos = new Vector2(posX, posY + 1);
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
        else
        {
            spawnPos = new Vector2(posX, posY);
        }

        bool acid = false;
        if (playerNum > 0) // only not if bullets make bullets
        {
            if (length > players[playerNum - 1].range)
            {

                length = players[playerNum - 1].range;
                Debug.Log("NICK SHOT " + length);
            }
            if (players[playerNum - 1].playerClass == 3 && players[playerNum - 1].special)
            {
                length += 2;

            }
            if (players[playerNum - 1].playerClass == 2 && players[playerNum - 1].special)
            {
                length = players[playerNum - 1].range;
            }
            if (players[playerNum - 1].playerClass == 1 && players[playerNum - 1].special)
            {
                length -= 1;
            }
        }
        else if (length > range)
        {
            length = range;
        }
        else if (range == 1)
        {
            acid = true;
            length = 5;
        }
        if (range == 0)
        {
            UpdateBullets();
        }

        if (playerNum > 0)
        {

            Debug.Log(players[playerNum - 1].special);
            if (players[playerNum - 1].special)
            {

                bullets.Add(new Bullet(dir, length, players[playerNum - 1].playerClass));
            }
            else
            {
                bullets.Add(new Bullet(dir, length));
            }
        }
        else
        {
            bullets.Add(new Bullet(dir, length));
            Debug.Log("ADDED");
        }

        if (!acid)
        {
            bullets[bullets.Count - 1].body = Instantiate(bulletGO, spawnPos, Quaternion.identity);
        }
        else
        {
            bullets[bullets.Count - 1].body = Instantiate(acidTile, spawnPos, Quaternion.identity);

        }
        //bullets[bullets.Count - 1].body.transform.parent = bulletManager.transform;


        if (bullets[bullets.Count - 1].type == 3)
        {
            MovePlayer(playerNum, GetRecoilDir(dir), true);
        }

        for (int p = 0; p < players.Count; p++)
        {
            if (bullets[bullets.Count - 1].body.transform == players[p].character.transform)
            {
                KillPlayer(players[p].playerNumber);
            }
        }

        gunShotSound.Play();
    }

    public void ShootPlayer(int p, int dir)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].playerNumber == p)
            {
                MakeBullet(dir, new Vector2(players[i].character.transform.position.x, players[i].character.transform.position.y), p);
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

    public void PlayerSpecial(bool active, int num)
    {
        if (active)
        {
            players[num - 1].special = true;
        }
        else
        {
            players[num - 1].special = false;
        }
    }

    private int GetRecoilDir(int dir)
    {
        if (dir == 1)
        {
            return 3;
        }
        else if (dir == 2)
        {
            return 4;
        }
        else if (dir == 3)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    public int GetAmmo(int p)
    {
        return players[p - 1].ammo;
    }

    public void UseAmmo(int ammoCost, int p)
    {
        players[p - 1].ammo -= ammoCost;
    }

    public int GetClass(int p)
    {
        return players[p - 1].playerClass;
    }
    public void SetSpecial(int p)
    {
        players[p - 1].special = true;
    }

    public void SetAmmoPiles(GameObject ammoPileParent)
    {
        foreach (Transform child in ammoPileParent.transform)
        {
            ammoPiles.Add(child);
        }
    }

    private int GetTShotDir(int dir, int side)
    {
        if (dir == 1)
        {
            if (side == 1)
            {
                return 4;
            }
            else
            {
                return 2;
            }
        }
        else if (dir == 2)
        {
            if (side == 1)
            {
                return 1;
            }
            else
            {
                return 3;
            }
        }
        else if (dir == 3)
        {
            if (side == 1)
            {
                return 2;
            }
            else
            {
                return 4;
            }
        }
        else
        {
            if (side == 1)
            {
                return 3;
            }
            else
            {
                return 1;
            }
        }
    }

    private void PlayFX(GameObject FX, Transform position)
    {
        GameObject particle = Instantiate(FX, new Vector3(position.position.x, position.position.y), Quaternion.identity);
        print("spawned");
        //Debug.Break();
        Destroy(particle, 1f);
    }

}
