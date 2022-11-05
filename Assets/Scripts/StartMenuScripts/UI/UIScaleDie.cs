using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UIScaleDie : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Variables
    private Vector3 newPos;

    // Scale Die by factor 2
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData is null)
        {
            throw new ArgumentNullException(nameof(eventData));
        }
        else
        {
            RectTransform newPosRect = (RectTransform)eventData.pointerEnter.transform;
            newPos = new Vector3(2, 2, 2);
            GameObject.FindGameObjectWithTag("UIDie").transform.localScale = newPos;
            eventData.selectedObject = eventData.pointerEnter;
        }
    }

    // Unscale Die
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData is null)
        {
            throw new ArgumentNullException(nameof(eventData));
        }
        else
        {
            newPos = new Vector3(1, 1, 1);
            GameObject.FindGameObjectWithTag("UIDie").transform.localScale = newPos;
            eventData.selectedObject = null;
        }
    }
}
