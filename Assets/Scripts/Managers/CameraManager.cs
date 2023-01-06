using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    /// <summary>
    /// The currently active CameraManager.
    /// </summary>
    public static CameraManager current;

    public CameraPosition[] positions;

    /// <summary>
    /// The currently set camera position.
    /// </summary>
    public CameraPosition currentPosition { get; private set; }


    private bool moving = false;
    private float speed = 70f;
    private float angularSpeed = 180f;
    private float accuracy = 0.01f;

    private void Start()
    {
        current = this;
    }

    private void Update()
    {
        moveTowardsCurrentPosition();
    }

    /// <summary>
    /// Sets the camera position by it's name. Throws an exception if the position doesn't exist.
    /// </summary>
    /// <param name="name"></param>
    public void setPositionByName(string name) //throws exception if postion does not exist
    {
        for (int i = 0; i < positions.Length; i++)
        {
            if(positions[i].name == name)
            {
                setPositionByIndex(i);
                return;
            }
        }

        throw new System.Exception("No Camera Position with the name " + name + " found.");
    }

    /// <summary>
    /// Sets the camera position by it's index.
    /// </summary>
    /// <param name="index"></param>
    public void setPositionByIndex(int index)
    {
        currentPosition = positions[index];
        moving = true;

        //transform.position = positions[index].pos;
        //transform.rotation = Quaternion.Euler(positions[index].rot);
    }

    private void moveTowardsCurrentPosition()
    {
        if(moving)
        {
            float posDistance = Vector3.Distance(transform.position, currentPosition.pos);
            transform.position = Vector3.Slerp(transform.position, currentPosition.pos, (speed + posDistance) * Time.deltaTime / posDistance);

            Quaternion targetRotation = Quaternion.Euler(currentPosition.rot);
            float rotDistance = Quaternion.Angle(transform.rotation, targetRotation);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (angularSpeed + 3 * rotDistance) * Time.deltaTime / rotDistance);

            if (Vector3.Distance(transform.position, currentPosition.pos) < accuracy && Quaternion.Angle(transform.rotation, targetRotation) < accuracy) moving = false;
        }
    }
}
