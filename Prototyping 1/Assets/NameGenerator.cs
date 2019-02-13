﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameGenerator : MonoBehaviour
{
    string[] vowels = new string[]{"a", "e", "i", "o", "u"};
    string[] consonants = new string[] { "b","c","d","f","g","h","k","l","m","n","p","r","t","w","y", "s"};

    List<int> numbers = new List<int>();
    List<string> names = new List<string>();

    List<int> surnumbers = new List<int>();
    List<string> surnames = new List<string>();

    string characterName;
    string characterSurname;
    string storage;
    string previous;

    public Text wholename;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Counter.rotaterCount == 3)
        {
            if (Input.GetKeyDown("joystick button 5"))
            {
                numbers.Clear();
                names.Clear();
                surnumbers.Clear();
                surnames.Clear();
                previous = storage;
                int nameLength = Random.Range(3, 6);
                int surnameLength = Random.Range(4, 8);
                for (int n = 0; n < nameLength; n++)
                {
                    //vowels
                    if (n > 0)
                    {
                        if (numbers[n - 1] == 0)
                        {
                            int rnd = Random.Range(0, 3);
                            if (rnd == 2)
                            {
                                rnd = 1;
                            }
                            numbers.Add(rnd);
                        }
                        //consonants
                        else if (numbers[n - 1] == 1)
                        {
                            int rnd = 0;
                            numbers.Add(rnd);
                        }
                    }
                    else
                    {
                        int rnd = Random.Range(0, 2);
                        numbers.Add(rnd);
                    }
                }
                for (int s = 0; s < surnameLength; s++)
                {
                    //vowels
                    if (s > 0)
                    {
                        if (surnumbers[s - 1] == 0)
                        {
                            int rnd1 = 1;
                            surnumbers.Add(rnd1);
                        }
                        //consonants
                        if (surnumbers[s - 1] == 1)
                        {
                            int rnd1 = 0;
                            surnumbers.Add(rnd1);
                        }
                    }
                    else
                    {
                        int rnd1 = Random.Range(0, 2);
                        surnumbers.Add(rnd1);
                    }
                }
                ConstructName();
                ConstructSurname();
                storage = characterName + " " + characterSurname;
                wholename.text = storage;
            }
            if (Input.GetKeyDown("joystick button 4"))
            {
                wholename.text = previous;
            }
        }
    }
    void ConstructName()
    {
        characterName = "";
        for(int i = 0; i < numbers.Count; i ++)
        {
            if (numbers[i] == 0)
            {
                names.Add(vowels[Random.Range(0, vowels.Length)]);
            }
            else if (numbers[i] == 1)
            {
                names.Add(consonants[Random.Range(0, consonants.Length)]);
            }
            names[0] = names[0].ToUpper();
            characterName = characterName + names[i];
        }
        
        Debug.Log(characterName);
    }
    void ConstructSurname()
    {
        characterSurname = "";
        for (int i = 0; i < surnumbers.Count; i++)
        {
            if (surnumbers[i] == 0)
            {
                surnames.Add(vowels[Random.Range(0, vowels.Length)]);
            }
            else if (surnumbers[i] == 1)
            {
                surnames.Add(consonants[Random.Range(0, consonants.Length)]);
            }
            surnames[0] = surnames[0].ToUpper();
            characterSurname = characterSurname + surnames[i];
        }
    }
}
