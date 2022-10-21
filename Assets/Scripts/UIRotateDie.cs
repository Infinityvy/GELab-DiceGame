using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotateDie : MonoBehaviour
{
    Vector3 rotVec = new Vector3(2, 7, 5);
    public float rotationSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(rotVec * rotationSpeed * Time.deltaTime);
    }
}
