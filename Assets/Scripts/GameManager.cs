using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Player[] players = new Player[2];

    

    void Start()
    {
        if (players[0] == null || players[1] == null) /*ERROR*/;
    }

    void Update()
    {
        
    }
}
