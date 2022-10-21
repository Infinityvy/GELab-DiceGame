using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Die : MonoBehaviour
{

    public int id = -1;

    protected int facecount;

    public string dieName;
    public string description;
}
