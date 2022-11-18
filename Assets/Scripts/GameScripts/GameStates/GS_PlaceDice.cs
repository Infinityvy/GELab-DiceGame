using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_PlaceDice : GameState
{
    // Privates:
    private bool initialized = false;
    private DieFieldGrid activeGrid;
    private Player activePlayer;
    private int currentX = 0, currentY = 0;

    // Publics:
    public Die activeDie;
    public Transform highlighter;
    public DieFieldGrid[] fieldGrids;
    public GameManager gameManager;

    public override void init()
    {
        activePlayer = gameManager.activePlayer;
        activeGrid = fieldGrids[activePlayer.playerID];

        //highlighter.localScale = Vector3.one * activeGrid.fieldGap * 0.2f;
        highlighter.gameObject.SetActive(false);
        CameraManager.cam.setPositionByName("Player" + activePlayer.playerID + "Grid");

        initialized = true;
    }

    public override void exit() {
        initialized = false;
    }

    private void Update() {
        if (!initialized) return;
        highlightField();
        if (Input.GetKeyDown(KeyCode.Mouse0)) placeDie();
    }

    private void highlightField() {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y - activeGrid.transform.position.y));
        for (int x = 0; x < GameManager.gridSize; x++) {
            for (int z = 0; z < GameManager.gridSize; z++) {
                Vector3 gridPos = activeGrid.getFieldWorldPosFromFieldMatrixPos(x, z);
                if(mouseWorldPos.x > gridPos.x - activeGrid.fieldGap * 0.5f && mouseWorldPos.x < gridPos.x + activeGrid.fieldGap * 0.5f &&
                   mouseWorldPos.z > gridPos.z - activeGrid.fieldGap * 0.5f && mouseWorldPos.z < gridPos.z + activeGrid.fieldGap * 0.5f)
                {
                    highlighter.gameObject.SetActive(true);
                    highlighter.position = activeGrid.getFieldWorldPosFromFieldMatrixPos(x, z);
                    currentX = x;
                    currentY = y;
                    return;
                }
            }
        }
        if (highlighter.gameObject.activeSelf) {
            highlighter.gameObject.SetActive(false);
        }
    }

    private void placeDie() {
        if (highlighter.gameObject.activeSelf)
            if (activePlayer.dieFields[currentX, currentY] == null) {
                activePlayer.dieFields[currentX, currentY] = activeDie;
                activeDie.transform.position = activeGrid.getFieldWorldPosFromFieldMatrixPos(currentX, currentY);
            }
        exit();
    }
}
