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

    public void moveDie(Transform die)
    {
        if (die != null) {
            float objectWidth = GetComponent<RectTransform>().rect.width * GetComponentInParent<Canvas>().transform.localScale.x;
            float newX = transform.position.x - objectWidth / 2;
            float newY = transform.position.y;
            die.position = Camera.main.ScreenToWorldPoint(new Vector3(newX, newY, 10)) + Vector3.left;
        }
    }
}
