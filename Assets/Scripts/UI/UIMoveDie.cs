using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UIMoveDie : MonoBehaviour, IPointerEnterHandler/*, IPointerExitHandler*/
{
    // Variables
    private float UIDieVectorX;
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
            if (newPosRect.position.x < 0) newPos = new Vector2(-8.25f, newPosRect.position.y);
            else newPos = new Vector2(6.5f, newPosRect.position.y);
            GameObject.FindGameObjectWithTag("UIDie").transform.position = newPos;
            eventData.selectedObject = eventData.pointerEnter;
        }
    }

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    if (eventData is null)
    //    {
    //        throw new ArgumentNullException(nameof(eventData));
    //    }
    //    else
    //    {
    //        Vector2 newPos = new Vector2(11, 0);
    //        GameObject.FindGameObjectWithTag("UIDie").transform.position = newPos;
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
