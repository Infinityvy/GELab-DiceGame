using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// The currently active GameManager.
    /// </summary>
    public static GameManager current;

    /// <summary>
    /// Wether or not the game is paused.
    /// </summary>
    public static bool paused = false;

    //public
    /// <summary>
    /// The side length of the die grids. Field amount will be gridSize * gridSize. (readonly)
    /// </summary>
    public static int gridSize { get; } = 3;


    public Player[] players = new Player[2];
    public int activePlayerID = 0;
    public Player activePlayer;

    /// <summary>
    /// An array of all GameStates that allows them to be called by their enum.
    /// </summary>
    public GameStateRef[] gameStateRefs;

    //private
    private int roundNr = 0;

    private Die testDie_D6;
    private Die testDie_D4;

    private void Awake()
    {
        current = this;
    }

    void Start()
    {
        players[0] = new Player(0);
        players[0].initDicePiles();
        players[1] = new Player(1);
        players[1].initDicePiles();

        activePlayer = players[activePlayerID];

        //TestRollingDice();
        //TestIdleDice();

        //testDie_D6 = new Die_Normal_Default_D6();
        //testDie_D6.init_Transform();
        //Die[] testDice = { testDie_D6 };

        callGameState(GameStateName.DrawDice, null);
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Calls a GameState.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="dice"></param>
    public void callGameState(GameStateName name, Die[] dice)
    {
        for (int i = 0; i < gameStateRefs.Length; i++)
        {
            if(name == gameStateRefs[i].name)
            {
                gameStateRefs[i].gameState.init(dice);
                return;
            }
        }

        throw new System.Exception("No GameState named \"" + name + "\" found.");
    }

    public void endRound()
    {
        roundNr++;
        activePlayer = players[(activePlayerID + 1) % 2];
        activePlayerID = activePlayer.playerID;

        //put win condition calculation here and end game if needed

        callGameState(GameStateName.DrawDice, null);
    }



    #region testing
    private void TestRollingDice()
    {
        Vector3 spawnPos = new Vector3(0, 4, -8);

        testDie_D6 = new Die_Normal_Default_D6(0);
        testDie_D6.init_Transform();

        testDie_D4 = new Die_Normal_Midroll_D4(0);
        testDie_D4.init_Transform();

        testDie_D6.roll(Vector3.forward * 20, Vector3.one * 20);
        testDie_D4.roll(Vector3.forward * 10, Vector3.one * 40);

        InvokeRepeating("readDiceData", 0.5f, 0.5f);
    }

    private void TestIdleDice()
    {
        Vector3 spawnPos = new Vector3(0, 4, 0);


        testDie_D6 = new Die_Normal_Default_D6(0);
        testDie_D6.init_Transform();

        testDie_D4 = new Die_Normal_Midroll_D4(0);
        testDie_D4.init_Transform();

        testDie_D6.setIdleRotation(true);
        testDie_D4.setIdleRotation(true);
    }
    #endregion

}