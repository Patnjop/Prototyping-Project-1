using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater1 : MonoBehaviour
{
    public List<GameObject> parts = new List<GameObject>();
    public string tagName;
    Rotater rotater;
    int previousCount;
    int activeCount = 0;
    bool active;

    // Start is called before the first frame update
    void Start()
    {
        rotater = gameObject.GetComponent<Rotater>();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            active = true;
        }
        if(active == true)
        { 
            parts[activeCount].SetActive(true);
            if (Input.GetKeyDown("d"))
            {
                previousCount = activeCount;
                if (activeCount > 1)
                {
                    activeCount = 0;
                }
                else
                {
                    activeCount++;
                }
                parts[previousCount].SetActive(false);
            }
            if (Input.GetKeyDown("a"))
            {
                previousCount = activeCount;
                if (activeCount < 1)
                {
                    activeCount = 2;
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
