using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die_Normal_Highroll_D6 : Die
{
    public override int id { get; } = 1;
    public override string meshName { get; } = "D6_Default";
    public override string dieName { get; } = "High Roller D6";
    public override string description { get; } = "Two 6-sides with the risk of getting a low-roll./nFace values: 1-1-2-3-6-6";

    protected override int facecount { get; } = 6;


    protected override void setActiveFaceValue() //calculates top face by comparing the height of the center point of all faces; highest point is the top face; sets face value accordingly
    {
        //faces in order: up (y = 1), down (y = -1), left (x = -1), right (x = 1), front (z = 1), back (z = -1)
        //face value is   1           6              1              6              2              3

        int highest = 1;
        float y = (transform.rotation * Vector3.up).y; //we assume the up face is on top by default.

        float currentY = (transform.rotation * Vector3.down).y; //default down face
        if (currentY > y)
        {
            y = currentY;
            highest = 6;
        }

        currentY = (transform.rotation * Vector3.left).y; //default left face
        if (currentY > y)
        {
            y = currentY;
            highest = 1;
        }

        currentY = (transform.rotation * Vector3.right).y; //default right face
        if (currentY > y)
        {
            y = currentY;
            highest = 6;
        }

        currentY = (transform.rotation * Vector3.forward).y; //default front face
        if (currentY > y)
        {
            y = currentY;
            highest = 2;
        }

        currentY = (transform.rotation * Vector3.back).y; //default back face
        if (currentY > y)
        {
            y = currentY;
            highest = 3;
        }

        activeFaceValue = highest;
    }
}
