using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_DrawDice : GameState
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CameraManager cameraManager;
    private Player activePlayer;

    private Die[] drawnDice;

    public override void init()
    {
        activePlayer = gameManager.activePlayer;
        
        drawnDice = new Die[3];


        for (int i = 0; i < drawnDice.Length; i++)
        {
            drawnDice[i] = activePlayer.drawDie();
            drawnDice[i].setIdleRotation(true);
            drawnDice[i].transform.position = Quaternion.Euler(cameraManager.currentPosition.rot) * Vector3.back + cameraManager.currentPosition.pos
                                              + Vector3.left * 0.5f * drawnDice.Length + i * Vector3.right;
        }


    }
}
