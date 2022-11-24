using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_ChooseDice : GameState
{

    //privates
    private Die[] dice;

    private float gapSize = 1.5f;
    private float animationSpeed = 0.3f;
    private bool movedDice = false;

    public override void init(Die[] dice)
    {
        this.dice = dice;
        CameraManager.current.setPositionByName("Bowl");

        for (int i = 0; i < dice.Length; i++)
        {
            //dice[i].transform.position = getTargetPosition(i);
            dice[i].rotateToActiveFace();
            Debug.Log(dice[i].dieName + " has rolled a " + dice[i].activeFaceValue);
        }

        InvokeRepeating("moveDice", 0.01f, 0.01f);
    }

    public override void exit()
    {
        GameManager.current.callGameState(GameStateName.PlaceDice, dice);
    }

    private Vector3 getTargetPosition(int i)
    {
        return Quaternion.Euler(CameraManager.current.currentPosition.rot) * Vector3.forward * 8 + CameraManager.current.currentPosition.pos
                                              + Vector3.back * dice.Length * gapSize + i * Vector3.forward * dice.Length * gapSize;
    }

    private void moveDice()
    {
        movedDice = true;
        for (int i = 0; i < dice.Length; i++)
        {
            Vector3 targetPos = getTargetPosition(i);
            if (!(Vector3.Distance(dice[i].transform.position, targetPos) < 0.001f))
            {
                movedDice = false;
            }

            dice[i].transform.position = Vector3.Lerp(dice[i].transform.position, targetPos, animationSpeed / Vector3.Distance(dice[i].transform.position, targetPos));
        }

        if (movedDice) CancelInvoke();
    }
}
