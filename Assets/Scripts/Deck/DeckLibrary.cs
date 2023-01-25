using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckLibrary : MonoBehaviour
{
    public Deck activeDeck;

    public Transform dieCardPrefab;

    public Transform dieListObjPrefab;

    public Transform diceCardsParent;

    public Die[] arrayMitDice = new Die[3] { new Die_Normal_Default_D6(0), new Die_Normal_Highroll_D6(0), new Die_Normal_Midroll_D4(0) };

    public Transform[] dieCards = new Transform[3];



    void Start()
    {
        int distance = 0;
        for (int i = 0; i < arrayMitDice.Length; i++)
        {
            dieCards[i] = Instantiate(dieCardPrefab, diceCardsParent.transform.position, Quaternion.identity, diceCardsParent);
            dieCards[i].position = new Vector3(260 + distance, 380, 0);
            dieCards[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = arrayMitDice[i].dieName;
            dieCards[i].GetChild(1).GetComponent<TextMeshProUGUI>().text = arrayMitDice[i].description;
            dieCards[i].GetComponent<Button>().onClick.AddListener(delegate { addDice(arrayMitDice[i]); });
            distance += 175;
        }
    }


    void Update()
    {

    }
    public void addDice(Die die)
    {
        activeDeck.addDie(die);
        Debug.Log(die.dieName);
    }

}
