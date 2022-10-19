using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName = "Player";
    public int playerID = -1;

    private Deck activeDeck;

    private List<Die> drawSack;
    private List<Die> discardSack;

    void Start()
    {
        
    }


    void Update()
    {
        
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
}
