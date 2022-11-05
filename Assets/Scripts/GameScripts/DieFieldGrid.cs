using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFieldGrid : MonoBehaviour
{
    [SerializeField] private float fieldGap = 1.0f;
    [SerializeField] private bool alwaysShowGizmos = false;

    private void OnDrawGizmosSelected()
    {
        if (alwaysShowGizmos) return;
        drawGizmos();
    }

    private void OnDrawGizmos()
    {
        if (!alwaysShowGizmos) return;
        drawGizmos();
    }

    private void drawGizmos()
    {
        for(int y = 0; y < 3; y++)
        {
            for(int x = 0; x < 3; x++)
            {
                if (y == 0 && x == 1) Gizmos.color = Color.red;
                else Gizmos.color = Color.green;
                Gizmos.DrawCube(getFieldWorldPosFromFieldMatrixPos(x, y), 0.3f * Vector3.one);
            }
        }
    }

    public Vector3 getFieldWorldPosFromFieldMatrixPos(int x, int y) //fields in 3x3 numbered 0-8
    {
        //offset of the fields from the field grid object (x, z), no y offset
        //(1, 1) (1, 0) (1, -1)
        //(0, 1) (0, 0) (0, -1)
        //(-1, 1)(-1, 0)(-1, -1)

        return transform.position + transform.rotation * new Vector3(1 - y, 0, 1 - x) * fieldGap;
    }
}
