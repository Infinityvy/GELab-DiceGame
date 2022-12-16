using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    public readonly int minDeckSize = 15;
    public readonly int maxDeckSize = 25;

    private List<Die> dice = new List<Die>();

    public void addDie(Die die)
    {
        if(dice.Count < maxDeckSize) dice.Add(die);
    }

    public void removeDie(Die die)
    {
        dice.Remove(die);
    }

    public Die getDie(int index)
    {
        return dice[index];
    }

    public int getSize()
    {
        return dice.Count;
    }
}
