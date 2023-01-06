using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverlayController : MonoBehaviour
{
    public Text roundNrText;

    public Text player0Text;
    private Text player0ScoreText;
    private Vector3 player0TextPosition;

    public Text player1Text;
    private Text player1ScoreText;
    private Vector3 player1TextPosition;

    private float inactivePlayerTextScale = 0.6f;

    void Awake()
    {
        player0ScoreText = player0Text.transform.GetChild(0).GetComponent<Text>();
        player1ScoreText = player1Text.transform.GetChild(0).GetComponent<Text>();

        player0TextPosition = player0Text.transform.position;
        player1TextPosition = player1Text.transform.position;
    }

    private void Start()
    {
        updateScore();
    }

    public void updateScore()
    {
        player0ScoreText.text = "Score: " + GameManager.current.players[0].totalScore.ToString();
        player1ScoreText.text = "Score: " + GameManager.current.players[1].totalScore.ToString();
    }

    public void setActivePlayerDisplay(int playerId)
    {
        if (playerId == 0)
        {
            player0Text.transform.position = player0TextPosition;
            player1Text.transform.position = player1TextPosition;

            player0Text.transform.localScale = Vector3.one;
            player1Text.transform.localScale = Vector3.one * inactivePlayerTextScale;
        }
        else if (playerId == 1)
        {
            player1Text.transform.position = player0TextPosition;
            player0Text.transform.position = player1TextPosition;

            player1Text.transform.localScale = Vector3.one;
            player0Text.transform.localScale = Vector3.one * inactivePlayerTextScale;
        }
        else throw new System.Exception("The is no Player " + playerId + ".");
    }

    public void setRoundNr(int nr)
    {
        roundNrText.text = nr.ToString();
    }
}
