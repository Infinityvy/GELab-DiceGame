using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UIMoveDie : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
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
            Vector2 newPos = new Vector2((float)-8.25, newPosRect.position.y);
            GameObject.FindGameObjectWithTag("UIDie").transform.position = newPos;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData is null)
        {
            throw new ArgumentNullException(nameof(eventData));
        }
        else
        {
            Vector2 newPos = new Vector2(11, 0);
            GameObject.FindGameObjectWithTag("UIDie").transform.position = newPos;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
