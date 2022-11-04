using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotateDie : MonoBehaviour
{
    Vector3 rotVec = new Vector3(5, 10, 0);
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotVec * rotationSpeed * Time.deltaTime);
    }
}
