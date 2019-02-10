﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotater : MonoBehaviour
{
    public List<GameObject> parts = new List<GameObject>();
    public string tagName;
    public Rotater1 rotater1;
    Rotater rotater;
    int previousCount;
    int activeCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        rotater = gameObject.GetComponent<Rotater>();
        foreach (GameObject objects in GameObject.FindGameObjectsWithTag(tagName))
        {
            parts.Add(objects);
            objects.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        parts[activeCount].SetActive(true);
        if (Input.GetKeyDown("joystick button 5"))
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
        if (Input.GetKeyDown("joystick button 4"))
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
        if (Input.GetKeyDown("joystick button 0"))
        {
            rotater.enabled = false;
            rotater1.enabled = true;
        }
    }
}