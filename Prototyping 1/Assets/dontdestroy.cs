using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontdestroy : MonoBehaviour
{
    static dontdestroy Dontdestroy = null;
    public bool dontDupe;

    void Awake()
    {
        if (dontDupe)
        {
            //Check if instance already exists
            if (Dontdestroy == null)
            {
                //if not, set instance to this
                Dontdestroy = this;
            }
            //If instance already exists and it's not this:
            else if (Dontdestroy != this)
            {
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);
            }
        }
        
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
