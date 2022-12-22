using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die_Normal_Highroll_D6 : Die
{
    public override int id { get; } = 1;
    public override string meshName { get; } = "D6_Default";
    public override string dieName { get; } = "High Roller D6";
    public override string description { get; } = "Better chance to roll a 6 but also a 1.\n\nFace values:\n1-1-2-3-6-6";

    protected override int facecount { get; } = 6;

    protected override Quaternion activeFaceRot { get; set; }

    public Die_Normal_Highroll_D6(int playerID)
    {
        this.playerID = playerID;
    }

    protected override void setActiveFaceValue() //calculates top face by comparing the height of the center point of all faces; highest point is the top face; sets face value accordingly
    {
        //faces in order: top (y = 1), bottom (y = -1), left (x = -1), right (x = 1), front (z = 1), back (z = -1)
        //face value is   1            6                1              6              2              3

        int highest = 1;
        float y = (transform.rotation * Vector3.up).y; //we assume the top face is on top by default.
        activeFaceRot = Quaternion.Euler(0, 90, 0);

        float currentY = (transform.rotation * Vector3.down).y; //default bottom face
        if (currentY > y)
        {
            y = currentY;
            highest = 6;
            activeFaceRot = Quaternion.Euler(180, 90, 0);
        }

        currentY = (transform.rotation * Vector3.left).y; //default left face
        if (currentY > y)
        {
            y = currentY;
            highest = 1;
            activeFaceRot = Quaternion.Euler(0, 0, -90);
        }

        currentY = (transform.rotation * Vector3.right).y; //default right face
        if (currentY > y)
        {
            y = currentY;
            highest = 6;
            activeFaceRot = Quaternion.Euler(0, 180, 90);
        }

        currentY = (transform.rotation * Vector3.forward).y; //default front face
        if (currentY > y)
        {
            y = currentY;
            highest = 2;
            activeFaceRot = Quaternion.Euler(-90, -90, 0);
        }

        currentY = (transform.rotation * Vector3.back).y; //default back face
        if (currentY > y)
        {
            y = currentY;
            highest = 3;
            activeFaceRot = Quaternion.Euler(90, 90, 0);
        }

        activeFaceValue = highest;
    }

    protected override void setFaceNumbers()
    {
        dieObject.numbers[0].text = "1"; //top
        dieObject.numbers[1].text = "6"; //bottom
        dieObject.numbers[2].text = "2"; //front
        dieObject.numbers[3].text = "3"; //back
        dieObject.numbers[4].text = "6"; //right
        dieObject.numbers[5].text = "1"; //left
    }
}
