using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die_Normal_Midroll_D4 : Die
{
    public override int id { get; } = 3;
    public override string meshName { get; } = "D4_Default";
    public override string dieName { get; } = "Mid Roller D4";
    public override string description { get; } = "Prevents low rolls at the cost of no high rolls.\n\nFace values:\n2-3-4-5";

    protected override Quaternion activeFaceRot { get; set; } 

    protected override int facecount { get; } = 4;

    public Die_Normal_Midroll_D4(int playerID)
    {
        this.playerID = playerID;
    }

    protected override void setActiveFaceValue() //calculates bottom face by comparing the height of the center point of all faces; lowest point is the bottom face; sets face value accordingly
    {
        //faces in order: back (z = -1), left (x = -1), right (x = 1), bottom (y = -1)
        //face value is   2              3              4               5

        int lowest = 2;
        float y = (transform.rotation * Quaternion.Euler(20f, 0f, 0f) * Vector3.back).y; //we assume the front face is on top by default.
        activeFaceRot = Quaternion.Euler(70f, 90f, 0f);

        float currentY = (transform.rotation * Quaternion.Euler(0f, 20f, -20f) * Vector3.left).y; //default left face
        if (currentY < y)
        {
            y = currentY;
            lowest = 3;
            activeFaceRot = Quaternion.Euler(-30f, -10, -70f);
        }

        currentY = (transform.rotation * Quaternion.Euler(0f, -20f, 20f) * Vector3.right).y; //default right face
        if (currentY < y)
        {
            y = currentY;
            lowest = 4;
            activeFaceRot = Quaternion.Euler(-30f, 190, 70f);
        }

        currentY = (transform.rotation * Vector3.down).y; //default down face
        if (currentY < y)
        {
            y = currentY;
            lowest = 5;
            activeFaceRot = Quaternion.Euler(180, 90, 0);
        }

        activeFaceValue = lowest;
    }

    protected override void setFaceNumbers()
    {
        dieObject.numbers[0].text = "5"; //bottom
        dieObject.numbers[1].text = "2"; //back
        dieObject.numbers[2].text = "4"; //right
        dieObject.numbers[3].text = "3"; //left
    }
}
