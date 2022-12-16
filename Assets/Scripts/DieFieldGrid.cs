using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DieFieldGrid : MonoBehaviour
{
    public float gapSize = 1.0f;
    public TextMeshPro[] rowScoreDisplays; //from left to right
    [SerializeField] private bool alwaysShowGizmos = false;
    [SerializeField] private MeshRenderer gridMesh;
    [SerializeField] private Material defaultMat;
    [SerializeField] private Material highlightMat;
    //[SerializeField] private Transform[] fieldObjects;

    private bool[] highlighted;
    private int gridMeshMatIndexOffset = 3; //the index at which the field materials start in the grid mesh's material array

    //public float intensity = 0.1f;
    //public float speed = 2;
    //private Vector3 defaultSize;

    private void Start()
    {
        //defaultSize = fieldObjects[0].localScale;
        highlighted = new bool[9];
        for (int i = 0; i < 9; i++)
        {
            highlighted[i] = false;
        }
    }

    private void Update()
    {
        //hightlightFields();
    }

    #region gizmos
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
                if (y == 0 && x == 1) Gizmos.color = new Color(1, 0, 0, 0.5f);
                else Gizmos.color = new Color(0, 1, 0, 0.5f);

                Vector3 pos = getFieldWorldPosFromFieldMatrixPos(x, y);
                Gizmos.DrawCube(pos, Vector3.one * gapSize * 0.9f);
                //fieldObjects[y * 3 + x].position = pos + Vector3.down * 0.6f;
            }
        }
    }
    #endregion

    public Vector3 getFieldWorldPosFromFieldMatrixPos(int x, int y) //fields in 3x3 numbered 0-8
    {
        //offset of the fields from the field grid object (x, z), no y offset
        //(1, 1) (1, 0) (1, -1)
        //(0, 1) (0, 0) (0, -1)
        //(-1, 1)(-1, 0)(-1, -1)

        return transform.position + transform.rotation * new Vector3(1 - y, 0, 1 - x) * gapSize;
    }

    public void setHighlightField(int x, int y, bool state)
    {
        highlighted[x + y * 3] = state;
        Material[] materials = gridMesh.materials;
        materials[gridMeshMatIndexOffset + x + y * 3] = (state ? highlightMat : defaultMat);
        gridMesh.materials = materials;
    }

    public bool getHighlightField(int x, int y)
    {
        return highlighted[x + y * 3];
    }

    //private void hightlightFields()
    //{
    //    for (int i = 0; i < 9; i++)
    //    {
    //        if (highlighted[i])
    //        {
    //            float increment = Mathf.Sin(Time.time * speed) * intensity;

    //            fieldObjects[i].localScale = new Vector3(defaultSize.x + increment, defaultSize.y + increment, defaultSize.z);
    //        }
    //        else fieldObjects[i].localScale = defaultSize;
    //    }
    //}
}
