using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// The currently active GameManager.
    /// </summary>
    public static GameManager current;

    /// <summary>
    /// Wether or not the game is paused.
    /// </summary>
    public static bool paused;

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

    public GameState activeGameState;

    public GameOverlayController gameOverlay;
    public GameObject gameEndedScreen;

    //private
    public int roundNr { get; private set; } = 0;

    private void Awake()
    {
        current = this;
    }

    void Start()
    {
        paused = false;

        players[0] = new Player(0);
        players[0].initDicePiles();
        players[1] = new Player(1);
        players[1].initDicePiles();

        activePlayer = players[activePlayerID];

        gameOverlay.setActivePlayerDisplay(0);

        callGameState(GameStateName.DrawDice, null);
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Calls a GameState by its name.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="dice"></param>
    public void callGameState(GameStateName name, Die[] dice)
    {
        for (int i = 0; i < gameStateRefs.Length; i++)
        {
            if(name == gameStateRefs[i].name)
            {
                activeGameState = gameStateRefs[i].gameState;
                gameStateRefs[i].gameState.init(dice);
                return;
            }
        }

        throw new System.Exception("No GameState named \"" + name + "\" found.");
    }

    /// <summary>
    /// Ends the round and checks if winning conditions are met. Otherwise starts the next round.
    /// </summary>
    public void endRound()
    {
        //put win condition calculation here and end game if needed

        bool player0Full = true;
        bool player1Full = true;
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if (players[0].dieFields[x, y] == null) player0Full = false;
                if (players[1].dieFields[x, y] == null) player1Full = false;
            }
        }

        if (player0Full || player1Full)
        {
            endGame();
            return;
        }

        
        activePlayer = players[(activePlayerID + 1) % 2];
        activePlayerID = activePlayer.playerID;
        gameOverlay.setActivePlayerDisplay(activePlayerID);

        if (activePlayerID == 0)
        {
            roundNr++;
            gameOverlay.setRoundNr(roundNr);
        }
            


        callGameState(GameStateName.DrawDice, null);
    }

    /// <summary>
    /// End the game and determines the winner.
    /// </summary>
    private void endGame()
    {
        CameraManager.current.setPositionByName("Player" + activePlayerID + "EagleEye");

        gameEndedScreen.SetActive(true);

        int scoreDifference = players[0].totalScore - players[1].totalScore;

        Text winnerText = gameEndedScreen.transform.GetChild(0).GetComponent<Text>();

        if (scoreDifference == 0)
        {
            winnerText.text = "It's a tie!";
            return;
        }

        string winnerMessage = " wins with a NARROW victory!";
        int absScoreDifference = Mathf.Abs(scoreDifference);

        if (absScoreDifference > 48) winnerMessage = " wins with a LANDSLIDE victory!";
        else if (absScoreDifference > 24) winnerMessage = " wins with an OVERWHELMING victory!";
        else if (absScoreDifference > 12) winnerMessage = " wins with DECENT victory!";


        if(scoreDifference < 0) //player 1 wins
        {
            winnerText.text = "Player 1" + winnerMessage;
        }
        else if(scoreDifference > 0) //player 0 wins
        {
            winnerText.text = "Player 0" + winnerMessage;
        }
    }
}