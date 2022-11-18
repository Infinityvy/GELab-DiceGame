using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuCredits : UIMenu {
    public GameObject CreditsDiceEmpty;

    public override void Init() {
        gameObject.SetActive(true);
        CreditsDiceEmpty.SetActive(true);
    }

    public override void Exit() {
        gameObject.SetActive(false);
        CreditsDiceEmpty.SetActive(false);
    }
}
