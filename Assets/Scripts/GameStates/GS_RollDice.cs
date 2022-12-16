using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_RollDice : GameState
{
    //publics
    public Transform rollPos;

    public AudioClip[] audioClips;

    //privates
    private Player activePlayer;
    private Die[] dice;

    private Quaternion currentRollRot;
    private Vector3 currentRollPos;

    private bool[] diceToMove;
    private float moveDelay = 0.4f;
    private float animationSpeed = 0.7f;

    private float maxSpreadAngle = 5;
    private float maxTorqueStrength = 25;
    private float minTossStrength = 20;
    private float maxTossStrength = 35;

    private float timeout = 10;

    public override void init(Die[] dice)
    {
        activePlayer = GameManager.current.activePlayer;
        CameraManager.current.setPositionByName("Player" + activePlayer.playerID + "Bowl");
        this.dice = dice;

        GetComponent<AudioSource>().clip = audioClips[Random.Range(0, audioClips.Length)];
        GetComponent<AudioSource>().Play();

        currentRollRot = rollPos.rotation;
        currentRollPos = rollPos.position;

        if (activePlayer.playerID == 1)
        {
            currentRollRot = Quaternion.Euler(currentRollRot.eulerAngles.x, -currentRollRot.eulerAngles.y, currentRollRot.eulerAngles.z);
            currentRollPos.x = -currentRollPos.x;
        }

        diceToMove = new bool[dice.Length];

        for (int i = 0; i < dice.Length; i++)
        {
            diceToMove[i] = false;
        }

        StartCoroutine("animateMove");
    }

    public override void exit()
    {
        GameManager.current.callGameState(GameStateName.ChooseDice, dice);
    }

    private IEnumerator animateMove()
    {
        InvokeRepeating("moveDice", 0.01f, 0.01f);

        for (int i = 0; i < dice.Length; i++)
        {
            diceToMove[i] = true;
            yield return new WaitForSeconds(moveDelay);
        }
    }

    private void moveDice()
    {

        for (int i = 0; i < dice.Length; i++)
        {
            if (diceToMove[i])
            {
                dice[i].transform.position = Vector3.Lerp(dice[i].transform.position, currentRollPos, animationSpeed / Vector3.Distance(dice[i].transform.position, currentRollPos));

                if (Vector3.Distance(dice[i].transform.position, currentRollPos) < 0.001f)
                {
                    if (i == dice.Length - 1)
                    {
                        CancelInvoke();
                        StartCoroutine("waitForRollingDice");
                    }

                    diceToMove[i] = false;

                    dice[i].setIdleRotation(false);
                    dice[i].roll(currentRollRot * Quaternion.Euler(Random.Range(0, maxSpreadAngle),
                                                                     Random.Range(0, maxSpreadAngle),
                                                                     Random.Range(0, maxSpreadAngle)) * Vector3.forward * Random.Range(minTossStrength, maxTossStrength),
                                                                     new Vector3(Random.Range(0, maxTorqueStrength),
                                                                                 Random.Range(0, maxTorqueStrength),
                                                                                 Random.Range(0, maxTorqueStrength)));
                }
            }
        }
    }



    private IEnumerator waitForRollingDice()
    {
        float startTime = Time.time;
        bool allStoppedMoving = false;

        while(!allStoppedMoving && timeout > Time.time - startTime)
        {
            yield return new WaitForSeconds(0.5f);
            allStoppedMoving = true;
            for(int i = 0; i < dice.Length; i++)
            {
                if(!dice[i].hasStoppedMoving())
                {
                    allStoppedMoving = false;
                    continue;
                }
            }
        }

        if(!allStoppedMoving)
        {
            for (int i = 0; i < dice.Length; i++)
            {
                dice[i].forceStopMoving();
            }
        }

        exit();
    }
}
