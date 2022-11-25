using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableBreathEffect : MonoBehaviour
{
    public Material mat;
    public float intensity = 0.1f;
    public float speed = 1;

    private float defaultHeight = 0.04f;

    private void Start()
    {
        mat.SetFloat("_Parallax", defaultHeight);
    }

    void Update()
    {
        float increment = Mathf.Sin(Time.time * speed) * intensity;
        mat.SetFloat("_Parallax", defaultHeight + increment);
    }
}
