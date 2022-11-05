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
            float newX = transform.position.x - objectWidth / 200 - 0.8f; // die breite bekomme ich nicht hin.
            float newY = transform.position.y;
            die.transform.position = new Vector2(newX, newY);
        }
    }
}
