using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_ChooseDice : GameState
{

    //privates
    private Die[] dice;
    private int selectedDieIndex = -1;

    private float gapSize = 1.5f;
    private float animationSpeed = 0.3f;
    private bool movedDice = false;

    //tooltip
    public Transform tooltip;
    private bool initiated = false;

    public override void init(Die[] dice)
    {
        this.dice = dice;
        CameraManager.current.setPositionByName("Bowl");

        for (int i = 0; i < dice.Length; i++)
        {
            dice[i].rotateToActiveFace();
            Debug.Log(dice[i].dieName + " has rolled a " + dice[i].activeFaceValue);
        }

        InvokeRepeating("moveDice", 0.01f, 0.01f);

        initiated = true;
    }

    public override void exit()
    {
        initiated = false;
        tooltip.gameObject.SetActive(false);
        Die[] chosenDie = { dice[selectedDieIndex] };
        GameManager.current.callGameState(GameStateName.PlaceDice, chosenDie);
    }

    private void Update()
    {
        if (!initiated) return;

        highlightDie();

        if(Input.GetKeyDown(KeyCode.Mouse0) && selectedDieIndex != -1)
        {
            exit();
        }
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

    private void highlightDie()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.y - dice[0].transform.position.y)));

        for (int i = 0; i < dice.Length; i++)
        {
            if (Vector3.Distance(dice[i].transform.position, mouseWorldPos) < gapSize * 1.5f)
            {
                selectedDieIndex = i;
                tooltip.gameObject.SetActive(true);
                tooltip.position = Camera.main.WorldToScreenPoint(dice[i].transform.position + Vector3.left);

                tooltip.GetComponentInChildren<Tooltip>().title.text = dice[i].dieName;
                tooltip.GetComponentInChildren<Tooltip>().description.text = dice[i].description;

                return;
            }
        }

        selectedDieIndex = -1;
        tooltip.gameObject.SetActive(false);
    }
}
