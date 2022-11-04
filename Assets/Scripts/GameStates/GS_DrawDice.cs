using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_DrawDice : GameState
{
    private GameManager gameManager;
    private Player activePlayer;

    public override void init(GameManager gm)
    {
        gameManager = gm;
        activePlayer = gameManager.activePlayer;

    }
}
