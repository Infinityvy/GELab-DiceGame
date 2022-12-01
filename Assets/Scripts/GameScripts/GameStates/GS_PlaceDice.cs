using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_PlaceDice : GameState
{
    // Privates:
    private bool initialized = false;
    private Player activePlayer;
    private Die activeDie;
    private DieFieldGrid activeGrid;

    private int currentX = 0, currentY = 0;
    private bool fieldSelected = false;

    private bool eagleEyeActive = false;
    private bool enemyBoardEyeActive = false;
    private bool boardEyeActive = false;
    private float exitDelaySeconds = 1;

    // Publics:
    public Transform highlighter;
    public DieFieldGrid[] fieldGrids;

    public override void init(Die[] dice)
    {
        activeDie = dice[0];
        activePlayer = GameManager.current.activePlayer;
        activeGrid = fieldGrids[activePlayer.playerID];

        //highlighter.gameObject.SetActive(false);
        CameraManager.current.setPositionByName("Player" + activePlayer.playerID + "Grid");

        initialized = true;
    }

    public override void exit()
    {
        //highlighter.gameObject.SetActive(false);
        fieldSelected = false;
        eagleEyeActive = false;
        enemyBoardEyeActive = false;

        GameManager.current.endRound();
    }

    private void Update() 
    {
        if (!initialized) return;
        highlightField();

        activeDie.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y ,Camera.main.transform.position.y - activeGrid.transform.position.y));
       
        if (Input.GetKeyDown(KeyCode.Space)) toggleEagleEye();
        else if (Input.GetKeyDown(KeyCode.Q)) toggleEnemyBoardEye();
        else if (Input.GetKeyDown(KeyCode.E)) toggleBoardEye();

        if (Input.GetKeyDown(KeyCode.Mouse0)) placeDie();
    }

    private void highlightField() 
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y - activeGrid.transform.position.y));
        bool _fieldSelected = false;
        
        for (int x = 0; x < GameManager.gridSize; x++) {
            for (int y = 0; y < GameManager.gridSize; y++) {
                Vector3 gridPos = activeGrid.getFieldWorldPosFromFieldMatrixPos(x, y);

                if (mouseWorldPos.x > gridPos.x - activeGrid.gapSize * 0.5f && mouseWorldPos.x < gridPos.x + activeGrid.gapSize * 0.5f &&
                    mouseWorldPos.z > gridPos.z - activeGrid.gapSize * 0.5f && mouseWorldPos.z < gridPos.z + activeGrid.gapSize * 0.5f)
                {
                    //highlighter.gameObject.SetActive(true);
                    //highlighter.position = gridPos + Vector3.down * 0.6f;

                    currentX = x;
                    currentY = y;

                    if(!activeGrid.getHighlightField(x, y)) activeGrid.setHighlightField(x, y, true);
                    _fieldSelected = true;
                }
                else if (activeGrid.getHighlightField(x, y)) activeGrid.setHighlightField(x, y, false);
            }
        }

        fieldSelected = _fieldSelected;
        //if (!fieldSelected) {
        //    highlighter.gameObject.SetActive(false);
        //}
    }

    private void placeDie()
    {
        if (fieldSelected && activePlayer.dieFields[currentX, currentY] == null)
        {
            activePlayer.dieFields[currentX, currentY] = activeDie;
            activeDie.transform.position = activeGrid.getFieldWorldPosFromFieldMatrixPos(currentX, currentY);
            activeGrid.setHighlightField(currentX, currentY, false);

            activePlayer.calculateScores();
            for (int i = 0; i < 3; i++)
            {
                activeGrid.rowScoreDisplays[i].text = activePlayer.rowScores[i].ToString();
            }

            initialized = false;
            StartCoroutine("exitWithDelay");
        }
    }

    private IEnumerator exitWithDelay()
    {
        yield return new WaitForSeconds(exitDelaySeconds);
        exit();
    }

    #region camera toggles
    private void toggleEagleEye()
    {
        if(eagleEyeActive)
        {
            CameraManager.current.setPositionByName("Player" + activePlayer.playerID + "Grid");
            eagleEyeActive = false;
            boardEyeActive = true;
        }
        else
        {
            CameraManager.current.setPositionByName("Player" + activePlayer.playerID + "EagleEye");
            eagleEyeActive = true;
            enemyBoardEyeActive = false;
        }
    }

    private void toggleEnemyBoardEye()
    {
        if(enemyBoardEyeActive)
        {
            CameraManager.current.setPositionByName("Player" + activePlayer.playerID + "Grid");
            enemyBoardEyeActive = false;
            boardEyeActive = true;
        }
        else
        {
            CameraManager.current.setPositionByName("Player" + (activePlayer.playerID + 1) % 2 + "Grid");
            enemyBoardEyeActive = true;
            eagleEyeActive = false;
        }
    }

    private void toggleBoardEye()
    {
        if (boardEyeActive)
        {
            CameraManager.current.setPositionByName("Player" + activePlayer.playerID + "Grid");
            boardEyeActive = true;
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
