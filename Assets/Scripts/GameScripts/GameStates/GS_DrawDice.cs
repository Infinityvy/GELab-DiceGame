using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_DrawDice : GameState
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CameraManager cameraManager;
    private Player activePlayer;

    private Die[] drawnDice;
    private bool[] diceToAnimate;
    private float gapSize = 1.5f;

    private float animationDelay = 0.2f;
    private float animationSpeed = 0.01f;
    private int[] iterationsSinceAnimationStart;

    private Vector3 spawnPos = new Vector3(0, -10, -30);

    public override void init()
    {

        activePlayer = gameManager.activePlayer;
        
        drawnDice = new Die[3];
        diceToAnimate = new bool[drawnDice.Length];
        iterationsSinceAnimationStart = new int[drawnDice.Length];

        for (int i = 0; i < drawnDice.Length; i++)
        {
            diceToAnimate[i] = false;
            iterationsSinceAnimationStart[i] = 0;
        }

        for (int i = 0; i < drawnDice.Length; i++)
        {
            drawnDice[i] = activePlayer.drawDie();
            drawnDice[i].init_Transform();
            drawnDice[i].setIdleRotation(true);
            drawnDice[i].transform.position = spawnPos;
        }

        StartCoroutine("animateDraw");
    }

    private Vector3 getTargetPosition(int i)
    {
        return Quaternion.Euler(cameraManager.currentPosition.rot) * Vector3.forward * 8 + cameraManager.currentPosition.pos
                                              + Vector3.back * drawnDice.Length * gapSize + i * Vector3.forward * drawnDice.Length * gapSize;
    }

    private IEnumerator animateDraw()
    {
        InvokeRepeating("animateDice", 0.01f, 0.01f);

        for (int i = 0; i < drawnDice.Length; i++)
        {
            diceToAnimate[i] = true;
            yield return new WaitForSeconds(animationDelay);
        }
    }

    private void animateDice()
    {
        for (int i = 0; i < drawnDice.Length; i++)
        {
            if(diceToAnimate[i])
            {
                iterationsSinceAnimationStart[i]++;
                if(i == drawnDice.Length - 1 && Vector3.Distance(drawnDice[i].transform.position, getTargetPosition(i)) < 0.001f)
                {
                    CancelInvoke();

                    //for (int j = 0; j < drawnDice.Length; j++)
                    //{
                    //    drawnDice[j].setIdleRotation(true);
                    //}
                }

                drawnDice[i].transform.position = Vector3.Lerp(spawnPos, getTargetPosition(i), animationSpeed * iterationsSinceAnimationStart[i]);
            }
        }
    }
}
