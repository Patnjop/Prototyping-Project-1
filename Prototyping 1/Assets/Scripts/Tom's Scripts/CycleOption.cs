using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleOption : MonoBehaviour
{
    public int playerNumber;
    int cycleStep = 0;
    public Appearance playerAppearance = new Appearance();
    private CreatorManager creatorManager;
    private int currentHead = 0;
    private int currentBody = 0;
    private string storage;

    private void Start()
    {
        creatorManager = GetComponent<CreatorManager>();
        playerAppearance.playerNumber = playerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Joystick " + playerNumber + " Type 2"))
        {
            creatorManager.PlayerReady(playerAppearance, currentBody, currentHead);
        }

        if (Input.GetButtonDown("Joystick " + playerNumber + " Option 1"))
        {
            cycleStep += 1;
        }
        else if (Input.GetButtonDown("Joystick " + playerNumber + " Option 2"))
        {
            cycleStep -= 1;
        }
        cycleStep = Mathf.Clamp(cycleStep, 0, 3);

        if (Input.GetButtonDown("Joystick " + playerNumber + " Bumper R"))
        {
            if (cycleStep == 0)
            {
                changeName(1);
                
            }
            if (cycleStep == 1)
            {
                changeHead(1);
                
            }
            if (cycleStep == 2)
            {
                changeBody(1);
            }
            if (cycleStep == 3)
            {
                changeClass(1);
            }
        }
        else if (Input.GetButtonDown("Joystick " + playerNumber + " Bumper L"))
        {
            if (cycleStep == 0)
            {
                changeName(-1);
                
            }
            if (cycleStep == 1)
            {
                changeHead(-1);
                
            }
            if (cycleStep == 2)
            {
                changeBody(-1);
            }
            if (cycleStep == 3)
            {
                changeClass(-1);
            }
        }
    }

    private void changeHead(int change)
    {
        currentHead += change;
        currentHead = Mathf.Clamp(currentHead, 0, creatorManager.appearanceObjects[playerNumber-1].heads.Length - 1);
        playerAppearance.head = creatorManager.appearanceObjects[playerNumber-1].heads[currentHead];
        creatorManager.RefreshCharacter(playerAppearance, currentBody, currentHead);
    }

    private void changeBody(int change)
    {
        currentBody += change;
        currentBody = Mathf.Clamp(currentBody, 0, creatorManager.appearanceObjects[playerNumber - 1].bodyAccessories.Length - 1);
        playerAppearance.body = currentBody;
        creatorManager.RefreshCharacter(playerAppearance, currentBody, currentHead);
    }

    private void changeName(int change)
    {
        if (change > 0)
        {
            storage = playerAppearance.name;
            playerAppearance.name = creatorManager.nameGenerator.GetName();
        }
        else
        {
            playerAppearance.name = storage;
        }
        creatorManager.RefreshCharacter(playerAppearance, currentBody, currentHead);
    }

    private void changeClass(int change)
    {
        int current = playerAppearance.ability + change;
        current = Mathf.Clamp(current, 1, 4);
        playerAppearance.ability = current;
        creatorManager.RefreshCharacter(playerAppearance, currentBody, currentHead);
    }
}
