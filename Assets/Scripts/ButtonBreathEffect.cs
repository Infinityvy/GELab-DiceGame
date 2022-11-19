using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBreathEffect : MonoBehaviour
{
    private RectTransform rectTrans;

    private Vector3 defaultSize;
    public float intensity = 0.1f;
    public float speed = 2;

    private void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        defaultSize = rectTrans.localScale;
    }

    private void Update()
    {
        float increment = Mathf.Sin(Time.time * speed) * intensity;

        rectTrans.localScale = new Vector3(defaultSize.x + increment, defaultSize.y + increment, defaultSize.z);
    }
}
