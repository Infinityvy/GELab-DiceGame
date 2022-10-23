using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMoveDieCred : MonoBehaviour, IPointerEnterHandler
{
    Vector3 posCred = new Vector3((float)-8.25, 0, 0);

    // When pointer enters credits button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.FindGameObjectWithTag("Die").transform.position = posCred;
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
