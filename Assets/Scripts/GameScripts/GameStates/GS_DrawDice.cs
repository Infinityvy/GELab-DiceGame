using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_DrawDice : GameState
{
    private GameManager gameManager;
    private Player activePlayer;

    private Die[] drawnDice;

    public override void init(GameManager gm)
    {

        gameManager = gm;
        activePlayer = gameManager.activePlayer;

        drawnDice = new Die[3];

        for (int i = 0; i < drawnDice.Length; i++)
        {
            drawnDice[i] = activePlayer.drawDie();
            drawnDice[i].setIdleRotation(true);
        }


    }
}
