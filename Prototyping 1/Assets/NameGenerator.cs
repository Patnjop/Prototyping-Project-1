using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : MonoBehaviour
{
    string[] vowels = new string[]{"a", "e", "i", "o", "u" ,"s", "a", "e", "i", "o", "u" };
    string[] consonants = new string[] { "b","c","d","f","g","h","k","l","m","n","p","r","t","w","y"};

    List<int> numbers = new List<int>();
    List<string> names = new List<string>();

    string characterName;
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
                int nameLength = Random.Range(3, 6);
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
                        if (numbers[n - 1] == 1)
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
                ConstructName();
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
            names[0].ToUpper();
            characterName = characterName + names[i];
        }
        Debug.Log(characterName);
    }
}
