using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public
    //public static GameManager activeManager;

    public GameState gameState;
    public Player[] players = new Player[2];
    public int activePlayerID = 0;
    public Player activePlayer;

    public GameState[] gameStates;

    [SerializeField] private DieFieldGrid[] dieFields = new DieFieldGrid[2]; //the corresponding die fields for each player; 0 is for Player 0 and 1 is for Player 1
    [SerializeField] private DiceMesh[] diceMeshPrefabs;

    //private

    void Start()
    {
        //if (players[0] == null || players[1] == null) return/*ERROR*/;
        activePlayer = players[0];

        TestRollingDice();
    }

    void Update()
    {

    }

#region Game Loop Methods

    private void drawingDiceMode()
    {

    }

    private void rollingDiceMode()
    {

    }

    private void choosingDieMode()
    {

    }

    private void placingDieMode()
    {

    }

    private void checkingBoardMode()
    {

    }

#endregion

    private void placeDie(Player player, Die die, int x, int y)
    {
        if (player.dieFields[x, y] != null) return;

        die.transform.position = dieFields[player.playerID].getFieldWorldPosFromFieldMatrixPos(x, y);

        player.dieFields[x, y] = die;
    }

    private void removeDie(Player player, int x, int y)
    {
        if (player.dieFields[x, y] == null) return;

        player.discardDie(player.dieFields[x, y]);
        player.dieFields[x, y] = null;
    }

    Die testDie;
    Die testDie2;

    private void TestRollingDice()
    {
        Vector3 spawnPos = new Vector3(0, 4, -8);

        testDie = Instantiate(diceMeshPrefabs[0].diceMeshPrefab, spawnPos, Quaternion.identity).AddComponent<Die_Normal_Default_D6>();
        testDie2 = Instantiate(diceMeshPrefabs[1].diceMeshPrefab, spawnPos + Vector3.up * 3, Quaternion.identity).AddComponent<Die_Normal_Midroll_D4>();

        testDie.roll(Vector3.forward * 20, Vector3.one * 20);
        testDie2.roll(Vector3.forward * 10, Vector3.one * 40);

        InvokeRepeating("readDiceData", 0.5f, 0.5f);
    }

    private void readDiceData()
    {
        if(testDie.hasStoppedMoving())
        {
            Debug.Log(testDie.activeFaceValue);
            
        }
        else
        {
            Debug.Log("Die is moving with velocity: " + testDie.GetComponent<Rigidbody>().velocity);
        }

        if (testDie2.hasStoppedMoving())
        {
            Debug.Log(testDie2.activeFaceValue);

        }
        else
        {
            Debug.Log("Die is moving with velocity: " + testDie2.GetComponent<Rigidbody>().velocity);
        }
    }
}

[System.Serializable]
public class DiceMesh
{
    public string name;
    public GameObject diceMeshPrefab;
}