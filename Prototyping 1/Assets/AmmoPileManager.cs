using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPileManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerManager pMan = GameManager.GM.PManager();
        pMan.SetAmmoPiles(gameObject);
    }
}
