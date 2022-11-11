using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.rotation * Quaternion.Euler(20f, 0f, 0f) * Vector3.back);
        Gizmos.DrawLine(transform.position, transform.position + transform.rotation * Quaternion.Euler(0f, 20f, -20f) * Vector3.left);
        Gizmos.DrawLine(transform.position, transform.position + transform.rotation * Quaternion.Euler(0f, -20f, 20f) * Vector3.right);
    }
}
