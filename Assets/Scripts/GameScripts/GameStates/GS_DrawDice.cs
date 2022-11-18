using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GS_DrawDice : GameState
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Player activePlayer;
    [SerializeField] private GameObject ui_DrawDie;
    [SerializeField] private GameObject rollButton;

    private bool initialized = false;

    private Die[] drawnDice;
    private bool[] diceToAnimate;
    private float gapSize = 1.5f;


    //animation
    private float animationDelay = 0.2f;
    private float animationSpeed = 0.01f;
    private int[] iterationsSinceAnimationStart;

    private Vector3 spawnPos = new Vector3(0, -10, -30);

    //tooltip
    [SerializeField] private Transform tooltip;
    private Vector3 buttPos;

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

        buttPos = rollButton.transform.position;

        StartCoroutine("animateDraw");

        ui_DrawDie.SetActive(true);

        initialized = true;
    }
    public override void exit()
    {
        initialized = false;
        ui_DrawDie.SetActive(false);
    }

    private void Update()
    {
        if (!initialized) return;

        highlightDie();
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
                }

                drawnDice[i].transform.position = Vector3.Lerp(spawnPos, getTargetPosition(i), animationSpeed * iterationsSinceAnimationStart[i]);
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
