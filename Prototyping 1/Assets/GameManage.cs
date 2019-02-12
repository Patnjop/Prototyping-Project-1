using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    public Rotater1 rotater1;
    GameManage gameManage;
    public Text text;
    public static string selectedAbility;
    List<string> abilities = new List<string>();
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManage = GetComponent<GameManage>();
        abilities.Add("Recoil");
        abilities.Add("Incendinary");
        abilities.Add("T-Shot");
        abilities.Add("Wrapping bullets");
    }

    // Update is called once per frame
    void Update()
    {
        text.text = abilities[count];
        /*if (Input.GetKeyDown("joystick button 1") && Rotater1.selected == true)
        {
            Rotater1.selected = false;
            rotater1.enabled = true;
        }*/
        if (Counter.rotaterCount == 2)
        {
            if (Input.GetKeyDown("joystick button 5"))
            {
                if (count < 3)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }
            }
            if (Input.GetKeyDown("joystick button 4"))
            {
                if (count > 0)
                {
                    count--;
                }
                else
                {
                    count = 3;
                }
            }
        }
        if (Counter.rotaterCount == 3)
        {
            if (Input.GetKeyDown("joystick button 0"))
            {
                selectedAbility = abilities[count];
                Debug.Log(selectedAbility);
            }
        }
    }
}
