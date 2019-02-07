using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private bool awaitingInput = false;
    public TurnManager turnManager;
    public int turnAmount = 3;
    private int turnsMade = 0;

    public void GetInput()
    {
        awaitingInput = true;
    }

    private void Update()
    {
        if (awaitingInput == true)
        {
            //if (Input.GetAxisRaw("")
        }
    }
}
