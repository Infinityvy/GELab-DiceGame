using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UIMoveDie : MonoBehaviour, IPointerEnterHandler
{
    // Variables
    private Vector2 newPos;

    // When pointer enters button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData is null)
        {
            throw new ArgumentNullException(nameof(eventData));
        }
        else
        {
            RectTransform newPosRect = (RectTransform)eventData.pointerEnter.transform;
            if (newPosRect.position.x < 0) newPos = new Vector2(-7.5f, newPosRect.position.y);
            else newPos = new Vector2(4, newPosRect.position.y);
            GameObject.FindGameObjectWithTag("UIDie").transform.position = newPos;
            eventData.selectedObject = eventData.pointerEnter;
        }
    }
}
