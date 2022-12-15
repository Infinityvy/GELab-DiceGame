using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuPause : UIMenu {
    public override void Init() {
        gameObject.SetActive(true);
    }

    public override void Exit() {
        gameObject.SetActive(false);
    }
}
