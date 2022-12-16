using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuPause : UIMenu {
    public override void Init() 
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        GameManager.paused = true;
    }

    public override void Exit()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        GameManager.paused = false;
    }
}
