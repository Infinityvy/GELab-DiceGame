using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    //public
    public static int gridSize = 3;
    public GameState gameState;
    public Player[] players = new Player[2];
    public int activePlayerID = 0;
    public Player activePlayer;

    public GameStateRef[] gameStateRefs;

    //private
    [SerializeField] private DieFieldGrid[] dieFields = new DieFieldGrid[2]; //the corresponding die fields for each player; 0 is for Player 0 and 1 is for Player 1


    private Die testDie_D6;
    private Die testDie_D4;

    private void Awake()
    {
        current = this;
    }

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

        //testDie_D6 = new Die_Normal_Default_D6();
        //testDie_D6.init_Transform();
        //Die[] testDice = { testDie_D6 };

        callGameState(GameStateName.DrawDice, null);
    }

    void Update()
    {
        //reload scene
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.name);
        }
    }

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

    private void removeDie(Player player, int x, int y)
    {
        if (player.dieFields[x, y] == null) return;

        player.discardDie(player.dieFields[x, y]);
        player.dieFields[x, y] = null;
    }



    #region testing
    private void TestRollingDice()
    {
        Vector3 spawnPos = new Vector3(0, 4, -8);

        testDie_D6 = new Die_Normal_Default_D6();
        testDie_D6.init_Transform();

        testDie_D4 = new Die_Normal_Midroll_D4();
        testDie_D4.init_Transform();

        testDie_D6.roll(Vector3.forward * 20, Vector3.one * 20);
        testDie_D4.roll(Vector3.forward * 10, Vector3.one * 40);

        InvokeRepeating("readDiceData", 0.5f, 0.5f);
    }

    private void TestIdleDice()
    {
        Vector3 spawnPos = new Vector3(0, 4, 0);


        testDie_D6 = new Die_Normal_Default_D6();
        testDie_D6.init_Transform();

        testDie_D4 = new Die_Normal_Midroll_D4();
        testDie_D4.init_Transform();

        testDie_D6.setIdleRotation(true);
        testDie_D4.setIdleRotation(true);
    }
    #endregion

}

public enum GameStateName
{
    DrawDice, RollDice, ChooseDice, PlaceDice
}

[System.Serializable]
public struct GameStateRef
{
    public GameStateName name;
    public GameState gameState;
}