using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIUniversalButton : MonoBehaviour
{
    public void gotoMenu(UIMenu toMenu)
    {
        if (toMenu == null)
            return;
        toMenu.GetComponent<UIMenu>().Init();
        GetComponentInParent<UIMenu>().Exit();
    }

    public void moveDie(Transform die)
    {
        if (die != null) {
            EventSystem.current.SetSelectedGameObject(gameObject);
            float objectWidth = GetComponent<RectTransform>().rect.width * GetComponentInParent<Canvas>().transform.localScale.x;
            float newX = transform.position.x - objectWidth / 2;
            float newY = transform.position.y;
            die.position = Camera.main.ScreenToWorldPoint(new Vector3(newX, newY, 10)) + Vector3.left;
        }
    }
}
