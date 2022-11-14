using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string playerName = "Player";
    public int playerID = -1;

    public Die[,] dieFields = new Die[GameManager.gridSize, GameManager.gridSize];
    public int[] rowScores { get; protected set; } = new int[3];
    public int totalScore { get; protected set; } = 0;


    private Deck activeDeck;

    private List<Die> drawSack;
    private List<Die> discardSack;

    public Player(int ID)
    {
        activeDeck = new Deck();
        playerID = ID;

        for(int i = 0; i < 5; i++)
        {
            activeDeck.addDie(new Die_Normal_Default_D6());
            activeDeck.addDie(new Die_Normal_Highroll_D6());
            activeDeck.addDie(new Die_Normal_Midroll_D4());
        }
    }

    public void initDicePiles() //fills draw pile with active deck and empties discard pile
    {
        drawSack = new List<Die>();
        discardSack = new List<Die>();

        for (int i = 0, size = activeDeck.getSize(); i < size; i++)
        {
            drawSack.Add(activeDeck.getDie(i));
        }
    }

    public void swapSacks() //swaps draw sack and discard sack
    {
        List<Die> tmpSack = drawSack;
        drawSack = discardSack;
        discardSack = tmpSack;
    }

    public Die drawDie() //draws and removes die from draw sack
    {
        int rndIndex = Random.Range(0, drawSack.Count);
        Die drawnDie = drawSack[rndIndex];
        drawSack.RemoveAt(rndIndex);

        return drawnDie;
    }

    public void discardDie(Die die) //adds die to discard sack
    {
        discardSack.Add(die);
    }

    public void calculateScores() //calculates score per row and total score
    {

        totalScore = 0;
        for(int x = 0; x < 3; x++)
        {
            rowScores[x] = 0;

            for(int y = 0; y < 3; y++)
            {
                if(dieFields[x, y] != null)
                {
                    dieFields[x, y].calculateActiveScore(dieFields, x, y);
                    rowScores[x] += dieFields[x, y].activeScore;
                }
            }

            totalScore += rowScores[x];
        }
    }
}
