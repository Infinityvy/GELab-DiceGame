using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIMenu : MonoBehaviour
{
    // init finction of the menu to enable all gameobjects
    public abstract void Init();

    // exit function of the menu to disable all gameobjects
    public abstract void Exit();
}
