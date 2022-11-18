using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die_Normal_Default_D6 : Die
{
    public override int id { get; } = 0;
    public override string meshName { get; } = "D6_Default";
    public override string dieName { get; } = "Common D6";
    public override string description { get; } = "A basic 6-sided die.\nFace values:\n\n1-2-3-4-5-6";

    protected override int facecount { get; } = 6;

    protected override void setActiveFaceValue() //calculates top face by comparing the height of the center point of all faces; highest point is the top face; sets face value accordingly
    {
        //faces in order: top (y = 1), bottom (y = -1), left (x = -1), right (x = 1), front (z = 1), back (z = -1)
        //face value is   1            6                2              5              3              4

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
            highest = 2;
        }

        currentY = (transform.rotation * Vector3.right).y; //default right face
        if (currentY > y)
        {
            y = currentY;
            highest = 5;
        }

        currentY = (transform.rotation * Vector3.forward).y; //default front face
        if (currentY > y)
        {
            y = currentY;
            highest = 3;
        }

        currentY = (transform.rotation * Vector3.back).y; //default back face
        if (currentY > y)
        {
            y = currentY;
            highest = 4;
        }

        activeFaceValue = highest;
    }

    protected override void setFaceNumbers()
    {
        dieObject.numbers[0].text = "1"; //top
        dieObject.numbers[1].text = "6"; //bottom
        dieObject.numbers[2].text = "5"; //right
        dieObject.numbers[3].text = "2"; //left
        dieObject.numbers[4].text = "3"; //front
        dieObject.numbers[5].text = "4"; //back
    }
}
