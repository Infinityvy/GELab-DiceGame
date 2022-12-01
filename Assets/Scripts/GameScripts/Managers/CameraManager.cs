using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CameraPosition
{
    public string name;
    public Vector3 pos;
    public Vector3 rot;
}

public class CameraManager : MonoBehaviour
{
    public static CameraManager current;

    public CameraPosition[] positions;

    public CameraPosition currentPosition { get; private set; }

    private void Start()
    {
        current = this;
    }

    private void Update()
    {

    }

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

    public void setPositionByIndex(int index)
    {
        currentPosition = positions[index];

        transform.position = positions[index].pos;
        transform.rotation = Quaternion.Euler(positions[index].rot);
    }
}
