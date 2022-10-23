using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMoveDiePlay : MonoBehaviour, IPointerEnterHandler
{
    Vector3 posPlay = new Vector3((float)-8.25, (float)2.5, 0);

    // When pointer enters play button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.FindGameObjectWithTag("Die").transform.position = posPlay;
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
