using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatorManager : MonoBehaviour
{
    public AppearanceObjects[] appearanceObjects;
    public NameGenerator nameGenerator;
    private List<CycleOption> playerCycleOptions = new List<CycleOption>();
    [SerializeField] private GameObject[] playerUIInfo;
    private List<Player> players = new List<Player>();

    private void Update()
    {
        for (int i = 1; i < 5; i++)
        {
            if (Input.GetButtonDown("Joystick " + i + " Type 1"))
            {
                
                foreach (CycleOption cycleOption in playerCycleOptions)
                {
                    if (cycleOption.playerNumber != i)
                    {
                        continue;
                    }
                }

                playerCycleOptions.Add(gameObject.AddComponent<CycleOption>());
                playerCycleOptions[playerCycleOptions.Count - 1].playerNumber = i;
                playerUIInfo[i-1].SetActive(true);
                    
                
            }
        }
    }

    public void RefreshCharacter(Appearance appearance, int body, int head)
    {
        string abilityName = "";
        if (appearance.ability == 1)
        {
            abilityName = "Fire Bullet";
        }
        else if (appearance.ability == 2)
        {
            abilityName = "Wrap Bullet";
        }
        else if (appearance.ability == 3)
        {
            abilityName = "Recoil Bullet";
        }
        else if (appearance.ability == 4)
        {
            abilityName = "T Bullet";
        }
        appearanceObjects[appearance.playerNumber - 1].infoText.text = "Name: " + appearance.name + "\n\nHead Type: " + head + "\n\nBody Type: " + body + "\n\nAbility: " + abilityName;
        
        if (appearance.head)
        appearanceObjects[appearance.playerNumber - 1].headObject.GetComponent<Image>().sprite = appearance.head;

        if (appearance.body == 0)
        {
            for (int i = 0; i < appearanceObjects[appearance.playerNumber - 1].bodyAccessories.Length; i++)
            {
                if (i == 0)
                {
                    appearanceObjects[appearance.playerNumber - 1].bodyAccessories[i].SetActive(true);
                }
                else
                {
                    appearanceObjects[appearance.playerNumber - 1].bodyAccessories[i].SetActive(false);
                }
            }
        }
        else if (appearance.body == 1)
        {
            for (int i = 0; i < appearanceObjects[appearance.playerNumber - 1].bodyAccessories.Length; i++)
            {
                if (i == 1)
                {
                    appearanceObjects[appearance.playerNumber - 1].bodyAccessories[i].SetActive(true);
                }
                else
                {
                    appearanceObjects[appearance.playerNumber - 1].bodyAccessories[i].SetActive(false);
                }
            }
        }
        else if (appearance.body == 2)
        {
            for (int i = 0; i < appearanceObjects[appearance.playerNumber - 1].bodyAccessories.Length; i++)
            {
                if (i == 2)
                {
                    appearanceObjects[appearance.playerNumber - 1].bodyAccessories[i].SetActive(true);
                }
                else
                {
                    appearanceObjects[appearance.playerNumber - 1].bodyAccessories[i].SetActive(false);
                }
            }
        }
    }

    public void PlayerReady(Appearance appearance, int body, int head)
    {
        playerCycleOptions[appearance.playerNumber - 1].enabled = false;
        players.Add(new Player(appearance.playerNumber, appearance.name, head, body, appearance.ability));

        if (players.Count == playerCycleOptions.Count)
        {
            GameManager.GM.SetGameData(players);
        }
    }
}
