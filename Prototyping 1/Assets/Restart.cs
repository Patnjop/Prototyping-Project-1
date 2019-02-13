using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Restart : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe")); //new program
            Application.Quit(); //kill current process
        }
    }
}
