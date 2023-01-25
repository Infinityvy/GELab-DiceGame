using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTestScript : MonoBehaviour
{
    public bool is4Sided = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(is4Sided)
        {
            Gizmos.DrawLine(transform.position, transform.position + transform.rotation * Quaternion.Euler(20f, 0f, 0f) * Vector3.back);
            Gizmos.DrawLine(transform.position, transform.position + transform.rotation * Quaternion.Euler(0f, 20f, -20f) * Vector3.left);
            Gizmos.DrawLine(transform.position, transform.position + transform.rotation * Quaternion.Euler(0f, -20f, 20f) * Vector3.right);
            Gizmos.DrawLine(transform.position, transform.position + transform.rotation * Vector3.down);
        }
        else
        {
            Gizmos.DrawLine(transform.position, transform.position + transform.rotation * Vector3.up);
            Gizmos.DrawLine(transform.position, transform.position + transform.rotation * Vector3.down);
            Gizmos.DrawLine(transform.position, transform.position + transform.rotation * Vector3.left);
            Gizmos.DrawLine(transform.position, transform.position + transform.rotation * Vector3.right);
            Gizmos.DrawLine(transform.position, transform.position + transform.rotation * Vector3.forward);
            Gizmos.DrawLine(transform.position, transform.position + transform.rotation * Vector3.back);
        }
    }
}
