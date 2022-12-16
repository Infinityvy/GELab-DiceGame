using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollSpawnPointGizmo : MonoBehaviour
{
    public bool alwaysShowGizmos = false;

    private void OnDrawGizmos()
    {
        if(alwaysShowGizmos) drawGizmos();
    }

    private void OnDrawGizmosSelected()
    {
        if (!alwaysShowGizmos) drawGizmos();
    }

    private void drawGizmos()
    {
        Gizmos.color = new Color(0.2f, 0.2f, 1, 1);
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + transform.rotation * Vector3.forward * 10);
    }
}
