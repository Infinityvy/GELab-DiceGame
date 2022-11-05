using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUniversalButton : MonoBehaviour
{
    public void gotoMenu(GameObject menu)
    {
        if (menu != null) {
            menu.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void moveDie(GameObject die)
    {
        if (die != null) {
            float objectWidth = GetComponent<RectTransform>().rect.width;
            float newX = transform.localPosition.x - objectWidth / 2 - 100;
            float newY = transform.localPosition.y;
            die.transform.localPosition = new Vector2(newX, newY);
        }
    }
}
