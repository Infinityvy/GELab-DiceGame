using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Die : MonoBehaviour
{
    public readonly int id = -1;

    public readonly string dieName;
    public readonly string description;
}
