using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBreathEffect : MonoBehaviour
{

    private Vector3 defaultSize;
    public float intensity = 0.1f;
    public float speed = 2;

    private void Start()
    {
        defaultSize = transform.localScale;
    }

    private void Update()
    {
        float increment = Mathf.Sin(Time.time * speed) * intensity;

        transform.localScale = new Vector3(defaultSize.x + increment, defaultSize.y + increment, defaultSize.z);
    }
}
