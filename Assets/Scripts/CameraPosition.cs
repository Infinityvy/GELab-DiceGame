using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A position and rotation connected to a name. Can be used to change the cameras position and rotation using the CameraManager.
/// </summary>
[System.Serializable]
public struct CameraPosition
{
    public string name;
    public Vector3 pos;
    public Vector3 rot;
}
