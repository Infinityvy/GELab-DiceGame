using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuStart : UIMenu {
    public GameObject StartDie;
    
    public override void Init() {
        gameObject.SetActive(true);
        StartDie.SetActive(true);
    }
    
    public override void Exit() {
        gameObject.SetActive(false);
        StartDie.SetActive(false);
    }
}
