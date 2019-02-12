using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public static int rotaterCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0"))
        {
            rotaterCount++;
        }
        if (Input.GetKeyDown("joystick button 1"))
        {
            if (rotaterCount > 0)
            {
                rotaterCount--;
            }
        }
    }
}
