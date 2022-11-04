using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUniversalButton : MonoBehaviour
{
    public void gotoMenu(GameObject menu)
    {
        menu.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }

    public float distance;

    public void moveDie(GameObject die)
    {
        RectTransform thisRect = GetComponent<RectTransform>();
        float newX = thisRect.position.x; 
        float newY = thisRect.position.y;
        Vector2 dieRect = Camera.current.WorldToScreenPoint(new Vector2(newX, newY));
        Debug.Log(dieRect);
        dieRect.x -= thisRect.rect.width / 2;
        Debug.Log(thisRect.rect.width);
        die.transform.position = Camera.current.ScreenToWorldPoint(dieRect) + Vector3.forward * 10;
    }
}
