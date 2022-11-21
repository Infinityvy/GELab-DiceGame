using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public
    public static int gridSize = 3;
    public GameState gameState;
    public Player[] players = new Player[2];
    public int activePlayerID = 0;
    public Player activePlayer;

    public GameState[] gameStates;

    [SerializeField] private DieFieldGrid[] dieFields = new DieFieldGrid[2]; //the corresponding die fields for each player; 0 is for Player 0 and 1 is for Player 1
    [SerializeField] private DiceMesh[] diceMeshes;


    private Die testDie_D6;
    private Die testDie_D4;

    //private

    void Start()
    {
        //if (players[0] == null || players[1] == null) return/*ERROR*/;
        players[0] = new Player(0);
        players[0].initDicePiles();
        players[1] = new Player(1);
        players[1].initDicePiles();

        activePlayer = players[0];

        //TestRollingDice();
        //TestIdleDice();
        testDie_D6 = new Die_Normal_Default_D6();
        testDie_D6.init_Transform();
        ((GS_PlaceDice)gameStates[3]).activeDie = testDie_D6;
        gameStates[3].init();
    }

    void Update()
    {

    }

    private void removeDie(Player player, int x, int y)
    {
        if (player.dieFields[x, y] == null) return;

        player.discardDie(player.dieFields[x, y]);
        player.dieFields[x, y] = null;
    }

    private void TestRollingDice()
    {
        Vector3 spawnPos = new Vector3(0, 4, -8);

        testDie_D6 = new Die_Normal_Default_D6();
        testDie_D6.init_Transform(Instantiate(diceMeshes[0].diceMeshPrefab, spawnPos, Quaternion.identity).transform);

        testDie_D4 = new Die_Normal_Midroll_D4();
        testDie_D4.init_Transform(Instantiate(diceMeshes[1].diceMeshPrefab, spawnPos + Vector3.up * 3, Quaternion.identity).transform);

        testDie_D6.roll(Vector3.forward * 20, Vector3.one * 20);
        testDie_D4.roll(Vector3.forward * 10, Vector3.one * 40);

        InvokeRepeating("readDiceData", 0.5f, 0.5f);
    }

    private void TestIdleDice()
    {
        Vector3 spawnPos = new Vector3(0, 4, 0);


        testDie_D6 = new Die_Normal_Default_D6();
        testDie_D6.init_Transform(Instantiate(getDiceMeshByName("D6_Default").diceMeshPrefab, spawnPos, Quaternion.identity).transform);

        testDie_D4 = new Die_Normal_Midroll_D4();
        testDie_D4.init_Transform(Instantiate(getDiceMeshByName("D4_Default").diceMeshPrefab, spawnPos + Vector3.up * 3, Quaternion.identity).transform);

        testDie_D6.setIdleRotation(true);
        testDie_D4.setIdleRotation(true);
    }

    private DiceMesh getDiceMeshByName(string name) //returns null if nothing was found
    {
        for(int i = 0; i < diceMeshes.Length; i++)
        {
            if (diceMeshes[i].name == name) return diceMeshes[i];
        }

        throw new System.Exception("No mesh with name " + name + " found.");
    }
}

[System.Serializable]
public class DiceMesh
{
    public string name;
    public GameObject diceMeshPrefab;
}