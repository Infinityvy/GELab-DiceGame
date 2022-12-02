using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DieObject : MonoBehaviour
{
    public TextMeshPro[] numbers; //front, back, top, bottom, right, left

    protected bool idleRotationEnabled = false;
    protected float rotationSpeed = 40;
    protected Vector3 rotVector;


    void Start()
    {
        
    }


    void Update()
    {
        if (idleRotationEnabled)
        {
            transform.Rotate(rotVector * Time.deltaTime);
        }
    }

    public void setIdleRotation(bool state)
    {
        idleRotationEnabled = state;
        transform.rotation = Quaternion.identity;

        if (state)
        {
            int xFac = 0;
            int zFac = 0;
            while (xFac == 0 && zFac == 0)
            {
                xFac = Random.Range(-1, 2);
                zFac = Random.Range(-1, 2);
            }

            rotVector = new Vector3(rotationSpeed * xFac, rotationSpeed, rotationSpeed * zFac);
        }
    }

    public void destroy()
    {
        
    }
}
