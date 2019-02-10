using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    public Rotater1 rotater1;
    public Text text;
    List<string> abilities = new List<string>();
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        abilities.Add("Recoil");
        abilities.Add("Incendinary");
        abilities.Add("T-Shot");
        abilities.Add("Wrapping bullets");
    }

    // Update is called once per frame
    void Update()
    {
        text.text = abilities[count];
        if (Input.GetKeyDown("joystick button 1") && Rotater1.selected == true)
        {
            rotater1.enabled = true;
        }
        if (Input.GetKeyDown("joystick button 5") && Rotater1.selected == true)
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
        if (Input.GetKeyDown("joystick button 4") && Rotater1.selected == true)
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
}
