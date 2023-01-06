using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_ChooseDice : GameState
{
    public GameObject currentUI;
    public AudioSource buttonAudioSource;
    public AudioClip[] audioClips;

    //privates
    private Die[] dice;
    private int selectedDieIndex = -1;

    private float gapSize = 1.5f;
    private float animationSpeed = 0.3f;
    private bool movedDice = false;
    private Player activePlayer;
    private CameraPosition alignementPos;

    private bool eagleEyeActive = false;
    private bool enemyBoardEyeActive = false;
    private bool boardEyeActive = false;

    //tooltip
    public Transform tooltip;
    private bool initiated = false;

    public override void init(Die[] dice)
    {
        this.dice = dice;
        activePlayer = GameManager.current.activePlayer;
        CameraManager.current.setPositionByName("Player" + activePlayer.playerID + "BowlTop");
        alignementPos = CameraManager.current.currentPosition;

        GetComponent<AudioSource>().clip = audioClips[Random.Range(0, audioClips.Length)];
        GetComponent<AudioSource>().Play();

        for (int i = 0; i < dice.Length; i++)
        {
            dice[i].rotateToActiveFace(activePlayer.playerID);
            //Debug.Log(dice[i].dieName + " has rolled a " + dice[i].activeFaceValue);
        }

        InvokeRepeating("moveDice", 0.01f, 0.01f);

        currentUI.SetActive(true);

        eagleEyeActive = false;
        enemyBoardEyeActive = false;
        boardEyeActive = false;

        initiated = true;
    }

    public override void exit()
    {
        initiated = false;
        movedDice = false;
        eagleEyeActive = false;
        enemyBoardEyeActive = false;
        boardEyeActive = false;

        buttonAudioSource.Play();
        tooltip.gameObject.SetActive(false);
        currentUI.SetActive(false);

        for (int i = 0; i < dice.Length; i++)
        {
            if (i != selectedDieIndex) GameManager.current.activePlayer.discardDie(dice[i]);
        }

        Die[] chosenDie = { dice[selectedDieIndex] };
        CancelInvoke();
        GameManager.current.callGameState(GameStateName.PlaceDice, chosenDie);
    }

    private void Update()
    {
        if (!initiated || GameManager.paused) return;

        if (!eagleEyeActive && !enemyBoardEyeActive && !boardEyeActive) highlightDie();
        else if (tooltip.gameObject.activeSelf || selectedDieIndex != -1)
        {
            tooltip.gameObject.SetActive(false);
            selectedDieIndex = -1;
        }

        if (Input.GetKeyDown(KeyCode.Space)) toggleEagleEye();
        else if (Input.GetKeyDown(KeyCode.Q)) toggleEnemyBoardEye();
        else if (Input.GetKeyDown(KeyCode.E)) toggleBoardEye();

        if (Input.GetKeyDown(KeyCode.Mouse0) && selectedDieIndex != -1) exit();
    }

    private Vector3 getTargetPosition(int i)
    {
        return Quaternion.Euler(alignementPos.rot) * Vector3.forward * 8 + alignementPos.pos
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

            dice[i].transform.position = Vector3.Slerp(dice[i].transform.position, targetPos, animationSpeed / Vector3.Distance(dice[i].transform.position, targetPos));
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
                tooltip.position = Camera.main.WorldToScreenPoint(dice[i].transform.position + Vector3.left * (1 - 2 * activePlayer.playerID));

                tooltip.GetComponentInChildren<Tooltip>().title.text = dice[i].dieName;
                tooltip.GetComponentInChildren<Tooltip>().description.text = dice[i].description;

                return;
            }
        }

        selectedDieIndex = -1;
        tooltip.gameObject.SetActive(false);
    }

    #region camera toggles
    private void toggleEagleEye()
    {
        if (eagleEyeActive)
        {
            CameraManager.current.setPositionByName("Player" + activePlayer.playerID + "BowlTop");
            boardEyeActive = false;
            enemyBoardEyeActive = false;
            eagleEyeActive = false;
        }
        else
        {
            CameraManager.current.setPositionByName("Player" + activePlayer.playerID + "EagleEye");
            boardEyeActive = false;
            enemyBoardEyeActive = false;
            eagleEyeActive = true;
        }
    }

    private void toggleEnemyBoardEye()
    {
        if (enemyBoardEyeActive)
        {
            CameraManager.current.setPositionByName("Player" + activePlayer.playerID + "BowlTop");
            boardEyeActive = false;
            enemyBoardEyeActive = false;
            eagleEyeActive = false;
        }
        else
        {
            CameraManager.current.setPositionByName("Player" + (activePlayer.playerID + 1) % 2 + "Grid");
            boardEyeActive = false;
            enemyBoardEyeActive = true;
            eagleEyeActive = false;
        }
    }

    private void toggleBoardEye()
    {
        if (boardEyeActive)
        {
            CameraManager.current.setPositionByName("Player" + activePlayer.playerID + "BowlTop");
            boardEyeActive = false;
            enemyBoardEyeActive = false;
            eagleEyeActive = false;
        }
        else
        {
            CameraManager.current.setPositionByName("Player" + activePlayer.playerID + "Grid");
            boardEyeActive = true;
            enemyBoardEyeActive = false;
            eagleEyeActive = false;
        }
    }
    #endregion
}
