using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CreatorManager : MonoBehaviour
{
    public AppearanceObjects[] appearanceObjects;
    public NameGenerator nameGenerator;
    private List<CycleOption> playerCycleOptions = new List<CycleOption>();
    [SerializeField] private GameObject[] playerUIInfo;
    private List<Player> players = new List<Player>();
    [SerializeField] private GameObject[] readyCovers;
    [SerializeField] private AudioSource tickSound;
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            tickSound.Play();
        }

        for (int i = 1; i < 5; i++)
        {
            if (Input.GetButtonDown("Joystick " + i + " Type 1"))
            {

                bool playerIn = false;

                foreach (CycleOption cycleOption in playerCycleOptions)
                {
                    if (cycleOption.playerNumber == i)
                    {
                        playerIn = true;
                    }
                }

                if (!playerIn)
                {
                    playerCycleOptions.Add(gameObject.AddComponent<CycleOption>());
                    playerCycleOptions[playerCycleOptions.Count - 1].playerNumber = i;
                    playerUIInfo[i - 1].SetActive(true);
                    playerCycleOptions = playerCycleOptions.OrderBy(o => o.playerNumber).ToList();
                }
                    
                
            }
        }
    }

    public void RefreshCharacter(Appearance appearance, int body, int head)
    {
        string abilityName = "";
        if (appearance.ability == 1)
        {
            abilityName = "Acid Shot (5)";
        }
        else if (appearance.ability == 2)
        {
            abilityName = "Wrap Shot (2)";
        }
        else if (appearance.ability == 3)
        {
            abilityName = "Recoil Shot (3)";
        }
        else if (appearance.ability == 4)
        {
            abilityName = "T Shot (5)";
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
        readyCovers[appearance.playerNumber - 1].SetActive(true);
        players.Add(new Player(appearance.playerNumber, appearance.name, head, body, appearance.ability));

        if (players.Count == playerCycleOptions.Count && players.Count > 1)
        {
            GameManager.GM.SetGameData(players);
        }
    }
}
