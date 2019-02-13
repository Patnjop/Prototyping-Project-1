using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater1 : MonoBehaviour
{
    public List<GameObject> parts = new List<GameObject>();
    public string tagName;
    public Rotater rotater;
    Rotater1 rotater1;
    int previousCount;
    int activeCount = 0;
    bool active;
    public static bool selected = false;

    // Start is called before the first frame update
    void Start()
    {
        rotater1 = gameObject.GetComponent<Rotater1>();
        foreach (GameObject objects in GameObject.FindGameObjectsWithTag(tagName))
        {
            parts.Add(objects);
            objects.SetActive(false);
        }
        parts[0].SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if(Counter.rotaterCount == 1)
        { 
            parts[activeCount].SetActive(true);
            if (Input.GetKeyDown("joystick button 5"))
            {
                previousCount = activeCount;
                if (activeCount >= parts.Count - 1)
                {
                    activeCount = 0;
                }
                else
                {
                    activeCount++;
                }
                parts[previousCount].SetActive(false);
            }
            if (Input.GetKeyDown("joystick button 4"))
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
