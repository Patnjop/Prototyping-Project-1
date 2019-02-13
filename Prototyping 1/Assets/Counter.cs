using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public static int rotaterCount1 = 0;
    public static int rotaterCount2 = 0;
    public static int rotaterCount3 = 0;
    public static int rotaterCount4 = 0;
    public int Controllernumber;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Joystick " + Controllernumber + " Option 1"))
        {
            if (Controllernumber == 1)
            {
                rotaterCount1++;
            }
            if (Controllernumber == 2)
            {
                rotaterCount2++;
            }
            if (Controllernumber == 3)
            {
                rotaterCount3++;
            }
            if (Controllernumber == 4)
            {
                rotaterCount4++;
            }
        }
        if (Input.GetButtonDown("Joystick " + Controllernumber + " Option 2"))
        {
            if (rotaterCount1 > 0 && Controllernumber == 1)
            {
                rotaterCount1--;
            }
            if (rotaterCount2 > 0 && Controllernumber == 2)
            {
                rotaterCount2--;
            }
            if (rotaterCount3 > 0 && Controllernumber == 3)
            {
                rotaterCount3--;
            }
            if (rotaterCount4 > 0 && Controllernumber == 4)
            {
                rotaterCount4--;
            }
        }
    }
}
