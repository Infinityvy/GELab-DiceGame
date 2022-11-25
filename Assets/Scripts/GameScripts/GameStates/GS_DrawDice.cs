using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GS_DrawDice : GameState
{
    private Player activePlayer;
    public GameObject currentUI;
    public GameObject rollButton;

    private bool initialized = false;

    private Die[] drawnDice;
    private bool[] diceToAnimate;
    private float gapSize = 1.5f;


    //animation
    private float animationDelay = 0.2f;
    private float animationSpeed = 0.7f;

    private Vector3 spawnPos = new Vector3(0, -10, -30);

    //tooltip
    public Transform tooltip;
    private Vector3 buttPos;

    public override void init(Die[] dice)
    {

        activePlayer = GameManager.current.activePlayer;
        
        drawnDice = new Die[3];
        diceToAnimate = new bool[drawnDice.Length];

        for (int i = 0; i < drawnDice.Length; i++)
        {
            diceToAnimate[i] = false;
        }

        for (int i = 0; i < drawnDice.Length; i++)
        {
            drawnDice[i] = activePlayer.drawDie();
            drawnDice[i].init_Transform();
            drawnDice[i].setIdleRotation(true);
            drawnDice[i].transform.position = spawnPos;
        }

        buttPos = rollButton.transform.position;

        StartCoroutine("animateDraw");

        currentUI.SetActive(true);

        initialized = true;
    }
    public override void exit()
    {
        initialized = false;
        CancelInvoke();

        for(int i = 0; i < drawnDice.Length; i++)
        {
            drawnDice[i].transform.position = getTargetPosition(i);
        }

        currentUI.SetActive(false);
        tooltip.gameObject.SetActive(false);
        GameManager.current.callGameState(GameStateName.RollDice, drawnDice);
    }

    private void Update()
    {
        if (!initialized) return;

        highlightDie();
    }

    private Vector3 getTargetPosition(int i)
    {
        return Quaternion.Euler(CameraManager.current.currentPosition.rot) * Vector3.forward * 8 + CameraManager.current.currentPosition.pos
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
                Vector3 targetPos = getTargetPosition(i);
                if (i == drawnDice.Length - 1 && Vector3.Distance(drawnDice[i].transform.position, targetPos) < 0.001f)
                {
                    CancelInvoke();
                }

                drawnDice[i].transform.position = Vector3.Lerp(drawnDice[i].transform.position, targetPos, animationSpeed / Vector3.Distance(drawnDice[i].transform.position, targetPos));
            }
        }
    }

    private void highlightDie()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.x - drawnDice[0].transform.position.x)));
        rollButton.transform.position = buttPos;

        for (int i = 0; i < drawnDice.Length; i++)
        {
            if(Vector3.Distance(drawnDice[i].transform.position, mouseWorldPos) < gapSize * 1.5f)
            {
                tooltip.gameObject.SetActive(true);
                tooltip.position = Camera.main.WorldToScreenPoint(drawnDice[i].transform.position + Vector3.down);

                tooltip.GetComponentInChildren<Tooltip>().title.text = drawnDice[i].dieName;
                tooltip.GetComponentInChildren<Tooltip>().description.text = drawnDice[i].description;

                if (i == 1) rollButton.transform.position = buttPos + Vector3.down * 80 * rollButton.transform.parent.parent.localScale.x;
                 
                return;
            }
        }

        tooltip.gameObject.SetActive(false);
    }
}
