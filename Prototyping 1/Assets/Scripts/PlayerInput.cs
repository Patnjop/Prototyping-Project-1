using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private bool awaitingInput = false;
    public TurnManager turnManager;
    public int playerNumberInt;
    private string playerNumber;
    public int turnAmount = 3;
    private int turnsMade = 0;
    private int moveTypeSelected = 0; // 0 in none, 1 is move, 2 is attack
    private int direction; // 1 up, 2 right, 3 down, 4 left

    private float timer;
    private float seconds;
    public int secondsTillSelect;

    private List<Move> moveList;

    


    public void GetInput()
    {

        awaitingInput = true;
        moveList = new List<Move>();
        playerNumber = playerNumberInt.ToString();
    }

    private int GetAnalogDirection()
    {
        int dir = 0;
        if (Mathf.Round(Input.GetAxisRaw("Joystick " + playerNumber + " Vertical")) == 1)
        {
            dir = 1;
        }
        else if (Mathf.Round(Input.GetAxisRaw("Joystick " + playerNumber + " Vertical")) == -1)
        {
            dir = 3;
        }
        else if (Mathf.Round(Input.GetAxisRaw("Joystick " + playerNumber + " Horizontal")) == 1)
        {
            dir = 2;
        }
        else if (Mathf.Round(Input.GetAxisRaw("Joystick " + playerNumber + " Horizontal")) == -1)
        {
            dir = 4;
        }
        else
        {
            dir = 0;
        }
        Debug.Log(dir);
        return dir;
    }

    

    private void Update()
    {
        if (awaitingInput == true)
        {

            if (Input.GetButtonDown("Joystick " + playerNumber + " Option 2"))//Pressed B
            {
                Debug.Log("Cleared");
                moveList = new List<Move>();
                turnsMade = 0;
                moveTypeSelected = 0;
                direction = 0;
                timer = 0;
                seconds = 0;
                turnManager.IncrementInfo(playerNumberInt, turnsMade);
            }

            if (turnsMade < turnManager.movesPerTurn)
            {

                if (moveTypeSelected == 0)
                {
                    Debug.Log("Awaiting move choice");
                    if (Input.GetButtonDown("Joystick " + playerNumber + " Type 1"))//Pressed X
                    {
                        moveTypeSelected = 1;
                        Debug.Log("Pressed X");
                    }
                    else if (Input.GetButtonDown("Joystick " + playerNumber + " Type 2"))//Pressed Y
                    {
                        moveTypeSelected = 2;
                        Debug.Log("Pressed Y");
                    }
                }
                else if (moveTypeSelected != 0)
                {

                    direction = GetAnalogDirection();
                    Debug.Log("Direction:" + direction);


                    if (direction != 0)
                    {
                        timer += Time.deltaTime;
                        seconds = timer % 60;
                        turnManager.FillCircle(playerNumberInt, seconds, secondsTillSelect);
                        

                        if (seconds >= secondsTillSelect)
                        {
                            Debug.Log("Null Ref Debug " + playerNumberInt + "," + turnsMade + "," + moveTypeSelected + "," + direction);
                            moveList.Add(new Move(playerNumberInt, turnsMade, moveTypeSelected, direction));
                            turnsMade += 1;
                            Debug.Log("Move added, moves made: " + turnsMade + " move type: " + moveTypeSelected + " direction: " + direction);
                            moveTypeSelected = 0;
                            direction = 0;
                            timer = 0;
                            seconds = 0;
                            turnManager.FillCircle(playerNumberInt, 0, secondsTillSelect);
                            turnManager.IncrementInfo(playerNumberInt, turnsMade);
                            
                        }
                    }
                    else
                    {
                        Debug.Log("Direction failed");
                        turnManager.FillCircle(playerNumberInt, 0, secondsTillSelect);
                        timer = 0;
                        seconds = 0;

                    }
                }
            }
            else
            {
                Debug.Log("3 turns made, press A");
                if (Input.GetButtonDown("Joystick " + playerNumber + " Option 1"))//Pressed A
                {
                    Debug.Log("Ready!");
                    turnManager.AddPlayerMoves(moveList, playerNumberInt);
                    moveList = new List<Move>();
                    awaitingInput = false;
                    turnsMade = 0;
                    moveTypeSelected = 0;
                    direction = 0;
                    timer = 0;
                    seconds = 0;
                }
            }
        }
    }
}
