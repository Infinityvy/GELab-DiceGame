using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die_Normal_Midroll_D4 : Die
{
    public override int id { get; } = 3;
    public override string dieName { get; } = "Mid Roller D4";
    public override string description { get; } = "A very average rolling die. Prevents low rolls at the cost of no high rolls. When rolled the bottom face is counts.\nFace values: 2-3-4-5";

    protected override int facecount { get; } = 4;

    protected override void setActiveFaceValue() //calculates bottom face by comparing the height of the center point of all faces; lowest point is the bottom face; sets face value accordingly
    {
        //faces in order: front (y = 1), left (y = -1), right (x = -1), bottom (x = 1)
        //face value is   2              3              4               5

        int lowest = 2;
        float y = (transform.rotation * Quaternion.Euler(-20f, 0f, 0f) * Vector3.forward).y; //we assume the front face is on top by default.

        float currentY = (transform.rotation * Quaternion.Euler(0f, -20f, -20f) * Vector3.left).y; //default left face
        if (currentY < y)
        {
            y = currentY;
            lowest = 3;
        }

        currentY = (transform.rotation * Quaternion.Euler(0f, 20f, 20f) * Vector3.right).y; //default right face
        if (currentY < y)
        {
            y = currentY;
            lowest = 4;
        }

        currentY = (transform.rotation * Vector3.down).y; //default down face
        if (currentY < y)
        {
            y = currentY;
            lowest = 5;
        }

        activeFaceValue = lowest;
    }
}
