using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    public abstract void init();
    public abstract void exit();
}
