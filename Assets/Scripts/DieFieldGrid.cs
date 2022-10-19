using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFieldGrid : MonoBehaviour
{
    public float fieldGap = 1.0f;
    public bool alwaysShowGizmos = false;

    private void OnDrawGizmosSelected()
    {
        if (alwaysShowGizmos) return;
        Gizmos.color = new Color(0.1f, 0.98f, 0.09f);

        for (int i = 0; i < 9; i++)
            Gizmos.DrawCube(getFieldPosFromFieldIndex(i), 0.3f * Vector3.one);
    }

    private void OnDrawGizmos()
    {
        if (!alwaysShowGizmos) return;
        Gizmos.color = new Color(0.1f, 0.98f, 0.09f);

        for (int i = 0; i < 9; i++)
            Gizmos.DrawCube(getFieldPosFromFieldIndex(i), 0.3f * Vector3.one);
    }

    private Vector3 getFieldPosFromFieldIndex(int index) //fields in 3x3 numbered 0-8
    {
        //offset of the fields from the field grid object (x, z), no y offset
        //(1, 1) (1, 0) (1, -1)
        //(0, 1) (0, 0) (0, -1)
        //(-1, 1)(-1, 0)(-1, -1)
        int x = index % 3;
        int y = index / 3;

        return transform.position + new Vector3(1 - y, 0, 1 - x) * fieldGap;
    }
}
