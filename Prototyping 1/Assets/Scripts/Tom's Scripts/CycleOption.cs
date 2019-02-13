using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleOption : MonoBehaviour
{
    public int playerNumber;
    int cycleStep = 0;
    public Appearance playerAppearance;
    public Component[] scriptsToCycle;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Joystick " + playerNumber + " Bumper R"))
        {
            if (cycleStep == 0)
            {
                changeHead();
            }
            if (cycleStep == 1)
            {

            }
            if (cycleStep == 2)
            {

            }
            if (cycleStep == 3)
            {

            }
        }
        if (Input.GetButtonDown("Joystick " + playerNumber + " Bumper L"))
        {

        }
    }

    void changeHead()
    {

    }
}
