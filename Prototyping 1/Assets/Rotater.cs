using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotater : MonoBehaviour
{
    public List<GameObject> parts = new List<GameObject>();
    public string tagName;
    public Rotater1 rotater1;
    Rotater rotater;
    Counter counter;
    int previousCount;
    int activeCount = 0;
    public int rotaterCount;
    public int Controllernumber;
    
    // Start is called before the first frame update
    void Start()
    {
        rotater = gameObject.GetComponent<Rotater>();
        foreach (GameObject objects in GameObject.FindGameObjectsWithTag(tagName + Controllernumber))
        {
            parts.Add(objects);
            objects.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        parts[activeCount].SetActive(true);
        if (Counter.rotaterCount1 == 0)
        {
            if (Input.GetButtonDown("Joystick " + Controllernumber + " Bumper R"))
            {
                previousCount = activeCount;
                if (activeCount == parts.Count - 1)
                {
                    activeCount = 0;
                }
                else
                {
                    activeCount++;
                }
                parts[previousCount].SetActive(false);
            }
            if (Input.GetButtonDown("Joystick " + Controllernumber + " Bumper L"))
            {
                previousCount = activeCount;
                if (activeCount == 0)
                {
                    activeCount = parts.Count - 1;
                }
                else
                {
                    activeCount--;
                }
                parts[previousCount].SetActive(false);
            }
        }      
    }
}
